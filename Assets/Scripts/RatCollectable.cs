using UnityEngine;

public class RatCollectible : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            Animator anim = other.GetComponent<Animator>();
            if (anim != null) anim.SetTrigger("Celebrate");
            Debug.Log("¡Rata recogida! +10 puntos");
            Destroy(gameObject); // desaparece la rata
            // Aquí luego podemos sumar puntos al marcador
        }
    }
}

