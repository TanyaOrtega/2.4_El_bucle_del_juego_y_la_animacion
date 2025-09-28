using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class BackgroundScaler : MonoBehaviour
{
    [Tooltip("Margen en porcentaje (0..0.5) para mantener los elementos importantes dentro del centro. Ej 0.08 = 8%")]
    [Range(0f, 0.5f)]
    public float safeMargin = 0.08f;

    void Start()
    {
        var sr = GetComponent<SpriteRenderer>();
        if (sr == null || sr.sprite == null) return;

        // tamaño del viewport en unidades mundo
        float worldScreenHeight = Camera.main.orthographicSize * 2f;
        float worldScreenWidth = worldScreenHeight * Screen.width / Screen.height;

        // dimensiones de la imagen en unidades del sprite 
        float spriteWidth = sr.sprite.bounds.size.x;
        float spriteHeight = sr.sprite.bounds.size.y;

        // factor de escala para cubrir ancho y alto (manteniendo aspect)
        float scaleX = worldScreenWidth / spriteWidth;
        float scaleY = worldScreenHeight / spriteHeight;
        float scale = Mathf.Max(scaleX, scaleY);

        // aplica escala
        transform.localScale = new Vector3(scale, scale, 1f);

        // opcional: centra la parte "segura" (mantiene elementos importantes en centro)
        // puedes ajustar safeMargin en inspector si quieres dejar margen visual
        Vector2 safeOffset = Vector2.zero;
        if (safeMargin > 0f)
        {
            // mover ligeramente para priorizar centro (no necesario en la mayoría de los casos)
            safeOffset = new Vector2(0f, -safeMargin * worldScreenHeight);
            transform.localPosition = new Vector3(transform.localPosition.x + safeOffset.x,
                                                  transform.localPosition.y + safeOffset.y,
                                                  transform.localPosition.z);
        }
    }
}
