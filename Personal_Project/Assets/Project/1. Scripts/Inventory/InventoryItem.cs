using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

public class InventoryItem : MonoBehaviour, IInteractable, IBeginDragHandler,IDragHandler,IEndDragHandler
{
    [Range(0,255) ][SerializeField] private float _imageAlpha;
    
    private const int CellWidth = 64;
    private const int CellHeight = 64;
    
    private ItemData _itemData;
    private RectTransform _rectTransform;
    private Image _image;

    public Transform _dragLayer;
    public Transform PositionAfterDrag;

    // ---------- Event Method ----------
    
    private void Awake() => CacheComponents();

    private void Start() => Init();

    // ----------------------------------
    
    private void CacheComponents()
    {
        _itemData = GetComponent<ItemData>();
        _rectTransform = GetComponent<RectTransform>();
        _image = GetComponent<Image>();
    }

    private void Init()
    {
        _rectTransform.sizeDelta = new Vector2(_itemData.WidthSize * CellWidth, _itemData.HeightSize * CellHeight);
        _image.sprite = _itemData.Icon;
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        PositionAfterDrag = transform.parent;
        transform.SetParent(_dragLayer);
        _image.color = new Color(255, 255, 255, _imageAlpha);
        _image.raycastTarget = false;
    }

    public void OnDrag(PointerEventData eventData)
    {
        transform.position = Mouse.current.position.ReadValue();
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        _image.color = new Color(255, 255, 255, 255);
        _image.raycastTarget = true;
        transform.SetParent(PositionAfterDrag);
    }

    public GameObject GameObject { get; }
    public RectTransform GrabPoint { get; }
}
