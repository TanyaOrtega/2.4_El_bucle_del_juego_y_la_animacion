using UnityEngine;
using UnityEngine.EventSystems;

[RequireComponent(typeof(RectTransform))]
public class UIDragPersistent : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    [Tooltip("RectTransform del DragLayer (panel vacío en el Canvas). Si queda vacío intentará encontrar uno llamado 'DragLayer'.")]
    public RectTransform dragParent;

    RectTransform rt;
    Canvas canvas;
    CanvasGroup cg;
    Vector2 normalizedPos = new Vector2(0.5f, 0.5f);
    Vector2 lastParentSize = Vector2.zero;
    bool isDragging = false;

    void Awake()
    {
        rt = GetComponent<RectTransform>();
        canvas = GetComponentInParent<Canvas>();

        // Reusar CanvasGroup si existe o añadirlo
        cg = GetComponent<CanvasGroup>();
        if (cg == null) cg = gameObject.AddComponent<CanvasGroup>();

        // si no asignaste dragParent en inspector, busca uno llamado "DragLayer" en el Canvas
        if (dragParent == null && canvas != null)
        {
            Transform t = canvas.transform.Find("DragLayer");
            if (t != null) dragParent = t as RectTransform;
        }

        if (dragParent != null) UpdateNormalizedFromAnchored();
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        isDragging = true;

        // Si aún no hay dragParent intenta encontrarlo
        if (dragParent == null && canvas != null)
        {
            Transform t = canvas.transform.Find("DragLayer");
            if (t != null) dragParent = t as RectTransform;
            else Debug.LogWarning("UIDragPersistent: no se encontró DragLayer. Crea un GameObject vacío llamado 'DragLayer' dentro del Canvas.");
        }

        // Reparentear al DragLayer para quitarlo del layout
        if (dragParent != null)
        {
            transform.SetParent(dragParent, false); // false => usar posiciones locales en el nuevo padre
            transform.SetAsLastSibling(); // visible encima
        }

        cg.blocksRaycasts = false; // dejar pasar raycasts mientras arrastra
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (dragParent == null || canvas == null) return;

        RectTransformUtility.ScreenPointToLocalPointInRectangle(dragParent, eventData.position, canvas.worldCamera, out Vector2 localPoint);
        rt.anchoredPosition = localPoint;

        UpdateNormalizedFromAnchored();
        lastParentSize = dragParent.rect.size;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        isDragging = false;
        cg.blocksRaycasts = true;
        // normalizedPos ya actualizado durante OnDrag
    }

    void Update()
    {
        if (dragParent == null) return;

        Vector2 curSize = dragParent.rect.size;
        if (!isDragging && (curSize != lastParentSize))
        {
            // Reubica manteniendo la misma posición relativa (normalized)
            rt.anchoredPosition = normalizedPos * curSize - (curSize * 0.5f);
            lastParentSize = curSize;
        }
    }

    void UpdateNormalizedFromAnchored()
    {
        if (dragParent == null) return;
        Vector2 size = dragParent.rect.size;
        if (size.x <= 0 || size.y <= 0) return;
        normalizedPos = (rt.anchoredPosition + (size * 0.5f)) / size;
    }
}
