using UnityEngine;

public class PlayerCollision : MonoBehaviour
{
    private SpriteRenderer sr;

    void Start()
    {
        sr = GetComponent<SpriteRenderer>();
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log("Colisión con: " + collision.gameObject.name);

        if (collision.gameObject.CompareTag("Wall") || collision.gameObject.CompareTag("Floor"))
        {
            if (sr != null) sr.color = Color.red;
        }
    }

    void OnCollisionExit2D(Collision2D collision)
    {
        if (sr != null) sr.color = Color.white;
    }
}

