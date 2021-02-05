using UnityEngine;

public class Explosion : MonoBehaviour
{
    [Header("法術碰撞")]
    public GameObject explosion;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Destroy(gameObject);
       GameObject temp =  Instantiate(explosion, transform.position, transform.rotation);
        Destroy(temp, 1.5f);
    }
    
   
}
