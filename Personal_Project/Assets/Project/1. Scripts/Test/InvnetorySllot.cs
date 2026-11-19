using UnityEngine;
using UnityEngine.EventSystems;

public class InvnetorySllot : MonoBehaviour, IDropHandler
{
    public void OnDrop(PointerEventData eventData)
    {
        if (transform.childCount == 0)
        {
            GameObject dropped = eventData.pointerDrag;
            DraggableItem draggableItem = dropped.GetComponent<DraggableItem>();
            draggableItem.ParentAfterDrag = transform;
        }
    }
}
