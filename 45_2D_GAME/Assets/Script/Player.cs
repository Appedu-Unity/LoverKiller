using UnityEngine;

public class Player : MonoBehaviour
{
    [Header("移動速度"), Range(0.1f,10)]
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
    [Header("生命數量"), Range(0, 10)]
    public int live = 3;

    private int score;
    private AudioSource aud;
    private Rigidbody2D rig;
    private Animator ani;

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
        
    }
    private void Awake()
    {
        //剛體 = 取得元件<剛體元件>();
        //抓到角色身上的剛體元件存放到 rig 欄位內
        rig = GetComponent<Rigidbody2D>();
        ani = GetComponent<Animator>();
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
        ani.SetBool("跑步開關",h != 0);
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
        if(Input.GetKeyDown(KeyCode.A))
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
            GameObject spellsIns = Instantiate(spells, point.transform.position, point.transform.rotation);
            spellsIns.GetComponent<Rigidbody2D>().AddForce(transform.right * Speedspelles);

            
        }
    }

    /// <summary>
    /// 死亡功能
    /// </summary>
    /// <param name="obj"></param>
    private void Dead(string obj)
    { 
    
    }
    
}
