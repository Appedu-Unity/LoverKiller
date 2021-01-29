using UnityEngine;

public class Door : MonoBehaviour
{
    private Gamemanager gm;
    private void Awake()
    {
        gm = FindObjectOfType<Gamemanager>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.tag.Equals("Player"))
        {

            if (gm.GetKeyNumbers() > 0)
            {
                gm.UseKey();
                GetComponent<BoxCollider2D>().enabled = false;
                Destroy(gameObject);
            }
        }
    }
}
