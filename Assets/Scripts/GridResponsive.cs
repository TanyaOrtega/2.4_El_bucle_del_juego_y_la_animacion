using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(GridLayoutGroup))]
public class GridResponsive : MonoBehaviour
{
    public GridLayoutGroup grid;
    public RectTransform containerRect; 
    public int colsLandscape = 3;
    public int colsPortrait = 1; 
    public float minCell = 80f;
    public float maxCell = 600f;

    float lastRatio = 0f;

    void Reset()
    {
        grid = GetComponent<GridLayoutGroup>();
    }

    void Start()
    {
        if (grid == null) grid = GetComponent<GridLayoutGroup>();
        ApplyLayout();
    }

    void Update()
    {
        float ratio = (float)Screen.width / Screen.height;
        if (Mathf.Abs(ratio - lastRatio) > 0.01f)
        {
            lastRatio = ratio;
            ApplyLayout();
        }
    }

    void ApplyLayout()
    {
        bool isPortrait = ((float)Screen.width / Screen.height) < 1f;
        int cols = isPortrait ? colsPortrait : colsLandscape;
        grid.constraint = GridLayoutGroup.Constraint.FixedColumnCount;
        grid.constraintCount = Mathf.Max(1, cols);

        // Calcula ancho disponible para celdas
        float paddingLR = grid.padding.left + grid.padding.right;
        float spacingTotal = grid.spacing.x * (cols - 1);
        float availableWidth = (containerRect != null ? containerRect.rect.width : Screen.width) - paddingLR - spacingTotal;
        float cellW = Mathf.Clamp(availableWidth / cols, minCell, maxCell);

        // Ajusta la celda (usa mismo alto para cuadrado)
        grid.cellSize = new Vector2(cellW, cellW);

        // Forzar rebuild para que haga el relayout inmediatamente
        Canvas.ForceUpdateCanvases();
        if (containerRect != null) LayoutRebuilder.ForceRebuildLayoutImmediate(containerRect);
    }
}
