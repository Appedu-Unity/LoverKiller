using UnityEngine;

public class monster : MonoBehaviour
{
    [Header("移動速度"), Range(0.01f, 1f)]
    public float speed = 0.01f;
    [Header("法術"), Tooltip("存放要生成的法術預製物")]
    public GameObject spells;
    [Header("法術生成點"), Tooltip("法術要生成的起始位址")]
    public GameObject point;
    [Header("法術速度"), Range(0, 5000)]
    public float speedspells = 3000;
    [Header("攻擊延遲")]
    public float attackDelay = 3;
    [Header("施展法術音效")]
    public AudioClip soundspells;
    [Header("追蹤範圍"), Range(0, 1000)]
    public float rangeTrack = 4.5f;
    [Header("攻擊範圍"), Range(1, 1000)]
    public float rangeAttack = 3.5f;
    [Header("死亡音效")]
    public AudioClip sounddead;

    public int live = 10;
    private AudioSource aud;
    public Transform player;
    private Rigidbody2D rig;
    private float timer = 0;
    private Animator ani;

    private void Awake()
    {
        //玩家變形 = 遊戲物件.尋找("玩家物件名稱").變形
        player = GameObject.Find("玩家").transform;
        rig = GetComponent<Rigidbody2D>();
        ani = GetComponent<Animator>();
        aud = GetComponent<AudioSource>();
    }
    private void Update()
    {
        Move();
    }
    /// <summary>
    /// 敵人移動
    /// </summary>

    private void Move()
    {
        if (player.position.x > transform.position.x)
        {
            transform.eulerAngles = Vector3.zero;
        }
        else
        {
            transform.eulerAngles = new Vector3(0, 180, 0);
        }

        float dis = Vector3.Distance(player.position, transform.position);
        if (dis < rangeAttack)
        {
            ani.SetBool("跑步開關", false);

            Spells();
        }
        else if (dis < rangeTrack)
        {
            ani.SetBool("跑步開關", true);

            Vector3 newPos = transform.position;
            if (player.transform.position.x != transform.position.x)
            {
                if (player.transform.position.x > transform.position.x)
                {
                    newPos.x += speed;
                }
                else
                {
                    newPos.x -= speed;
                }
            }
            if (player.transform.position.y != transform.position.y)
            {
                if (player.transform.position.y > transform.position.y)
                {
                    newPos.y += speed;
                }
                else
                {
                    newPos.y -= speed;
                }
            }
            transform.position = newPos;
        }
    }
    //敵人攻擊
    private void Spells()
    {
        rig.velocity = Vector3.zero;
        if (timer >= attackDelay)
        {
            ani.SetBool("攻擊開關", true);

            Vector3 spells1 = point.transform.rotation.eulerAngles;
            spells1.z += 20;
            GameObject spellsIns1 = Instantiate(spells, point.transform.position + new Vector3(0, 1, 0), Quaternion.Euler(spells1));
            spellsIns1.GetComponent<Rigidbody2D>().AddForce(spellsIns1.transform.right * speedspells);

            Vector3 spells2 = point.transform.rotation.eulerAngles;
            spells2.z -= 20;
            GameObject spellsIns2 = Instantiate(spells, point.transform.position + new Vector3(0, -1, 0), Quaternion.Euler(spells2));
            spellsIns2.GetComponent<Rigidbody2D>().AddForce(spellsIns2.transform.right * speedspells);

            GameObject spellsIns3 = Instantiate(spells, point.transform.position, point.transform.rotation);
            spellsIns3.GetComponent<Rigidbody2D>().AddForce(spellsIns3.transform.right * speedspells);
            timer = 0;
        }
        else
        {
            ani.SetBool("攻擊開關", false);
            timer += Time.deltaTime;
        }
    }
    /// <summary>
    /// 敵人死亡
    /// </summary>
    private void Dead()
    {
        aud.PlayOneShot(sounddead,2.5f);
        ani.SetBool("死亡開關", true);
        enabled = false;
        GetComponent<CapsuleCollider2D>().enabled = false;
        rig.Sleep();
        Destroy(gameObject, 2.5f);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.tag.Equals("法術"))
        {
            live--;
            if (live <= 0)
            {
                Dead();
            }
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = new Color(0, 0, 1, 0.3f);                //追蹤範圍顏色
        Gizmos.DrawSphere(transform.position, rangeTrack);      //追蹤範圍

        Gizmos.color = new Color(1, 0, 0, 0.3f);                //攻擊範圍顏色
        Gizmos.DrawSphere(transform.position, rangeAttack);     //攻擊範圍
    }


}
