using UnityEngine;

public class RatCollision : MonoBehaviour
{
    public int points = 10; // puntos que da la rata

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("¡Rata atrapada! +" + points + " puntos");
            Destroy(gameObject); // desaparece la rata
            // Aquí luego podemos sumar puntos a un marcador global
        }
    }
}
