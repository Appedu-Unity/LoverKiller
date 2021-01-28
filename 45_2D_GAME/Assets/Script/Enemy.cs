using UnityEngine;

public class Enemy : MonoBehaviour
{
    [Header("移動速度"), Range(0, 1000)]
    public float speed = 10.5f;
    [Header("法術"), Tooltip("存放要生成的法術預製物")]
    public GameObject spells;
    [Header("法術生成點"), Tooltip("法術要生成的起始位址")]
    public GameObject point;
    [Header("法術速度"), Range(0, 5000)]
    public float speedspells = 800;
    [Header("施展法術音效")]
    public AudioClip soundspells;
    [Header("追蹤範圍"), Range(0, 1000)]
    public float rangeTrack = 4.5f;
    [Header("攻擊範圍"), Range(1, 1000)]
    public float rangeAttack = 3.5f;

    public Transform player;
    private Rigidbody2D rig;


    private void Awake()
    {
        //玩家變形 = 遊戲物件.尋找("玩家物件名稱").變形
        player = GameObject.Find("玩家").transform;
        rig = GetComponent<Rigidbody2D>();
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
            Spells();
        }
        else if(dis < rangeTrack)
        {
            rig.velocity = transform.right * speed;
        }
    }
    //敵人攻擊
    private void Spells()
    {
        rig.velocity = Vector3.zero;
    }
    /// <summary>
    /// 敵人死亡
    /// </summary>
    private void Dead()
    { 
    
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = new Color(0, 0, 1, 0.3f);                //追蹤範圍顏色
        Gizmos.DrawSphere(transform.position, rangeTrack);      //追蹤範圍

        Gizmos.color = new Color(1, 0, 0, 0.3f);                //攻擊範圍顏色
        Gizmos.DrawSphere(transform.position, rangeAttack);     //攻擊範圍
    }

    
}
