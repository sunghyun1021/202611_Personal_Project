using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ItemInstance : MonoBehaviour, IBeginDragHandler,IDragHandler,IEndDragHandler
{
    [SerializeField] private ItemSO _itemSO;
    [SerializeField] private List<Vector2Int> _itemShape;
    [SerializeField] private List<Vector2Int> _itemPositionInGrid;
    [SerializeField] private GridView _gridView;

    private Vector2 _initialMousePosition;
    private Vector2 _initialAnchoredPosition;
    private Vector2Int _initialGridPosition;
    private int _cellSize;
    
    public ItemData Data;
    [HideInInspector] public Vector2 PrevPosition;
    [HideInInspector] public float PrevRotation;
    [HideInInspector] public Vector2Int PositionInGrid;
    [HideInInspector] public RectTransform RectTransform;
    
    
    // ---------- Event Method ----------

    private void Awake() => CacheComponents();

    private void Start() => Init();
    
    // ---------- private Method ----------

    private void CacheComponents()
    {
        Data = new  ItemData(_itemSO);
        RectTransform =  GetComponent<RectTransform>();
        _itemShape = Data.ItemShape;
        _itemPositionInGrid = Data.ItemPositionInGrid;
        _gridView = InventoryManager.Instance.GridView;
    }

    private void Init()
    {
        _cellSize = GridView.CellSize;
    }

    /// <summary>
    /// 아이템을 돌리고 기준점 0,0을 좌상단으로 다시 맞추는 함수
    /// </summary>
    /// <param name="cells"></param>
    /// <returns></returns>
    private List<Vector2Int> Rotate(List<Vector2Int> cells)
    {
        Vector2Int temp;
        int minX = int.MaxValue;
        int minY = int.MaxValue;
        for (int i = 0; i < cells.Count; i++)
        {
            temp = new(-cells[i].y, cells[i].x);
            cells[i] = temp;
            
            if (cells[i].x < minX)
            {
                minX = cells[i].x;
            }

            if (cells[i].y < minY)
            {
                minY = cells[i].y;
            }
        }

        for (int i = 0; i < cells.Count; i++)
        {
            int x = cells[i].x - minX;
            int y = cells[i].y - minY;

            cells[i] = new Vector2Int(x, y);
        }

        return cells;
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        _initialAnchoredPosition = RectTransform.anchoredPosition;
        _initialMousePosition = eventData.position;
        _initialGridPosition = _gridView.GetGridPoint(eventData.position);
        _gridView.StartItemDrag();
    }

    public void OnDrag(PointerEventData eventData)
    {
        RectTransform.anchoredPosition = eventData.position - _initialMousePosition + _initialAnchoredPosition;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        Vector2Int currentGridPosition = _gridView.GetGridPoint(eventData.position);
        
        float posX = _initialAnchoredPosition.x + (currentGridPosition - _initialGridPosition).x * _cellSize;
        float posY = _initialAnchoredPosition.y - (currentGridPosition - _initialGridPosition).y * _cellSize;
        
        RectTransform.anchoredPosition = new Vector2(posX, posY);
        _gridView.EndItemDrag();
    }
}
