using UnityEngine;

public class Door : MonoBehaviour
{

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.tag.Equals("Player"))
        {
            Player plr = collision.collider.GetComponent<Player>();
            if (plr.GetKeyNumbers() > 0)
            {
                plr.UseKey();
                GetComponent<BoxCollider2D>().enabled = false;
                Destroy(gameObject);
            }
        }
    }
}
