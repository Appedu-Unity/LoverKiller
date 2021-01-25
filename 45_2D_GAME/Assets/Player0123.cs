using UnityEngine;
public class Player0123 : MonoBehaviour
{
    [Header("Move Speed"), Range(0.01f, 1)]
    public float speed = 0.001f;

    [Header("Fire Point")]
    public GameObject point;

    [Header("Fire")]
    public GameObject fire;

    [Header("Fire Speed")]
    public float fireSpeed;



    private void Update()
    {
        Move();
        Fire();
    }

    private void Move()
    {
        Vector3 position = transform.position;

        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");

        if (h > 0)
        {
            position.x += speed;
        }
        else if (h < 0)
        {
            position.x -= speed;
        }

        if (v > 0)
        {
            position.y += speed;
        }
        else if (v < 0)
        {
            position.y -= speed;
        }

        transform.position = position;
    }

    private void Fire()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            GameObject fireIns = Instantiate(fire, point.transform.position, point.transform.rotation);
            fireIns.GetComponent<Rigidbody2D>().AddForce(transform.right * fireSpeed);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {

    }
}
