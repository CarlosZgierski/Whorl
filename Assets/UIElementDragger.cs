using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UIElementDragger : EventTrigger
{
    Vector2 Offset = new Vector2(0, 0);

    private bool dragging;

    public void Update()
    {
        if (dragging)
        {
            transform.position = new Vector2(Input.mousePosition.x, Input.mousePosition.y) + Offset;
        }
    }

    public override void OnPointerDown(PointerEventData eventData)
    {
        dragging = true;
        Offset = transform.position - Input.mousePosition;
    }

    public override void OnPointerUp(PointerEventData eventData)
    {
        dragging = false;
    }
}
