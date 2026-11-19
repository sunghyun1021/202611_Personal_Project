using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class DraggableItem : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    [HideInInspector] public Transform ParentAfterDrag;

    public Image Image;
    
    public void OnBeginDrag(PointerEventData eventData)
    {
        Debug.Log("Begin Drag");
        ParentAfterDrag = transform.parent;
        transform.SetParent(transform.root);
        transform.parent.SetAsFirstSibling();
        Image.raycastTarget = false;
    }

    public void OnDrag(PointerEventData eventData)
    {
        Debug.Log("On Drag");
        transform.position = Mouse.current.position.ReadValue();
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        Debug.Log("End Drag");
        transform.SetParent(ParentAfterDrag);
        Image.raycastTarget = true;
    }
}
