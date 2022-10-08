using UnityEngine;
using UnityEngine.UI;

public class GridIngemon : LayoutGroup
{
    private int rows;
    private int columns;
    private Vector2 size;
    public Vector2 spacing;
    
    public override void CalculateLayoutInputHorizontal()
    {
        base.CalculateLayoutInputHorizontal();
        float elements = Mathf.Sqrt(transform.childCount);
        rows = Mathf.FloorToInt(elements);
        columns = Mathf.CeilToInt(elements);

        Rect rt = rectTransform.rect;
        float height = rt.height;
        float width = rt.width;

        float cellWidth = (width - 2*spacing.x - padding.left - padding.right) / columns;
        float cellHeight = (height - 2*spacing.y - padding.top - padding.bottom) / rows;

        size = new Vector2(cellWidth, cellHeight);
        int columnCount = 0;
        int rowCount = 0;

        for (int i = 0; i < rectChildren.Count; i++)
        {
            rowCount = i / columns;
            columnCount = i % columns;

            RectTransform item = rectChildren[i];
            float xPos = (size.x + spacing.x) * columnCount + padding.left;
            float yPos = (size.y + spacing.y) * rowCount + padding.top;
            SetChildAlongAxis(item, 0, xPos, size.x);
            SetChildAlongAxis(item, 1, yPos, size.y);
        }
    }

    public override void CalculateLayoutInputVertical()
    {
        
    }

    public override void SetLayoutHorizontal()
    {
        
    }

    public override void SetLayoutVertical()
    {
        
    }
}
