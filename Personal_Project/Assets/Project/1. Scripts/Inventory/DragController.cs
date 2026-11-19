using UnityEngine;
using UnityEngine.EventSystems;

public class DragController : MonoBehaviour, IPointerDownHandler
{
    [field: SerializeField] public Transform DragLayer;
    
    public void OnPointerDown(PointerEventData eventData)
    {
        if (eventData.pointerCurrentRaycast.gameObject.TryGetComponent(out InventoryItem selectItem)) return;

        selectItem._dragLayer = DragLayer;
    }
}
