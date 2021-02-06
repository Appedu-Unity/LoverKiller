using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    [Header("移動速度"), Range(0.1f, 10)]
    public float speed = 0.1f;
    [Header("是否在地板上"), Tooltip("用來儲存玩家是否站在地板上")]
    public bool isGround = false;


    [Header("法術"), Tooltip("存放要生成的法術預置物")]
    public GameObject spells;
    [Header("法術生成點"), Tooltip("法術生成的起始位置")]
    public Transform point;
    [Header("法術速度"), Range(0, 500)]
    public int Speedspelles = 500;
    [Header("施法音效")]
    public AudioClip soundFire;
    public bool inPortal;


    private int score;
    private AudioSource aud;
    private Rigidbody2D rig;
    private Animator ani;

    private Gamemanager gm;

    void Update()
    {
        //移動
        Move();
        //射擊動作
        if (Input.GetMouseButton(0))
        {
            ani.SetBool("攻擊開關", true);
            Spell();
        }
        else
        {
            ani.SetBool("攻擊開關", false);
        }
        Nextlevel();
    }
    private void Awake()
    {
        //剛體 = 取得元件<剛體元件>();
        //抓到角色身上的剛體元件存放到 rig 欄位內
        rig = GetComponent<Rigidbody2D>();
        ani = GetComponent<Animator>();
        aud = GetComponent<AudioSource>();
        //透過<類型>取得物件
        //僅限於此(類型)在場景上只有一個
        gm = FindObjectOfType<Gamemanager>();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.name.Equals("傳送門") || collision.tag.Equals("Finish"))
        {
            inPortal = true;
            gm.showMessage(true);
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.name == "傳送門" || collision.tag.Equals("Finish"))
        {
            inPortal = false;
            gm.showMessage(false);
        }
    }

    private void Start()
    {

    }

    /// <summary>
    /// 移動功能
    /// </summary>
    private void Move()
    {
        //水平浮點數 = 輸入 的 取得軸向("水平") - 左右AD
        float h = Input.GetAxis("Horizontal");
        // 鋼體 的 速度 = 新 二為向量(水平浮點數 * 速度，剛體的加入度的y)
        rig.velocity = new Vector2(h * speed, rig.velocity.y);
        ani.SetBool("跑步開關", h != 0);
        //垂直浮點數 = 輸入 的 取得軸向("垂直") - 上下WS
        float v = Input.GetAxis("Vertical");
        // 剛體 的 速度 = 新 二為向量(垂直浮點數 * 速度，剛體的加速度的x)
        rig.velocity = new Vector2(v * speed, rig.velocity.x);
        ani.SetBool("跑步開關", v != 0);
        //走路方向向右
        if (Input.GetKeyDown(KeyCode.D))
        {
            transform.eulerAngles = new Vector3(0, 0, 0);
        }
        //走路方向向左
        if (Input.GetKeyDown(KeyCode.A))
        {
            transform.eulerAngles = new Vector3(0, 180, 0);
        }

        ani.SetBool("上跑開關", h > 0);
        ani.SetBool("下跑開關", h < 0);


    }

    /// <summary>
    /// 施法功能
    /// </summary>
    private void Spell()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {

            //音源 的 播放一次音效(音效，隨機大小聲)
            aud.PlayOneShot(soundFire, Random.Range(0.8f, 1.5f));
            //生成 子彈在槍口
            //生成(物件，座標，角度)
            GameObject spellsIns = Instantiate(spells, point.transform.position, point.transform.rotation);


            Vector3 mousePos = Input.mousePosition;
            mousePos.z = Camera.main.nearClipPlane;
            Vector3 worldPosition = Camera.main.ScreenToWorldPoint(mousePos);

            Vector3 directY = worldPosition - transform.position;
            directY.x = 0;
            directY.y = Mathf.Clamp(directY.y, -1f, 1f);


            //print("MO" + (worldPosition - transform.position));
            spellsIns.GetComponent<Rigidbody2D>().AddForce((transform.right + directY) * Speedspelles);
            //spellsIns.GetComponent<Rigidbody2D>().AddForce(transform.right * Speedspelles);

        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        string tag = collision.collider.tag;
        switch (tag)
        {
            case "敵人法術":
                if (gm.HurtAndCheckDead())
                {
                    Dead();
                    gm.ShowGameMenu(false);
                }
                break;
            case "Key":
                gm.addKey();
                Destroy(collision.collider.transform.gameObject);
                break;
            case "Speedup":
                speed += 2;
                Destroy(collision.collider.transform.gameObject);
                break;
            case "addLive":
                gm.addLive();
                Destroy(collision.collider.transform.gameObject);
                break;
            default:
                break;
        }
    }

    /// <summary>
    /// 死亡功能
    /// </summary>
    /// <param name="obj"></param>
    private void Dead()
    {
        //如果 死亡開關 為是 就 跳出
        if (ani.GetBool("死亡開關")) return;
        enabled = false;
        ani.SetBool("死亡開關", true);

        //if (Gamemanager.live > 1) Invoke("Replay", 2.5f);
    }

    private void Replay()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
    private void Nextlevel()
    {
        if (inPortal && Input.GetKeyDown(KeyCode.R))
        {
            int lvIndex = SceneManager.GetActiveScene().buildIndex;

            lvIndex++;

            SceneManager.LoadScene(lvIndex);

        }
    }
}
