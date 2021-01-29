using UnityEngine;

public class Magic : MonoBehaviour
{
    public int collisionCount = 2;
    public float magicDelay = 3;
    private float timer = 3;
    //private Rigidbody2D rig;
    private void Awake()
    {
        timer = magicDelay;
        //rig = GetComponent<Rigidbody2D>();
    }
    private void Update()
    {
        if (timer <= 0)
        {
            Destroy(gameObject);
        }
        else
        {
            timer -= Time.deltaTime;
        }

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        collisionCount--;
        if (collisionCount <= 0)
        {
            Destroy(gameObject);
        }
    }
}
