using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

public class GridView : MonoBehaviour
{
    public const int CellSize = 64;

    [SerializeField] private int _maxWidth;
    [SerializeField] private int _maxHeight;
    [SerializeField] private RectTransform _cellLayer;
    [SerializeField] private RectTransform _itemLayer;
    [SerializeField] private CellView[] _cells;
    [SerializeField] private GameObject _itemPrefab;

    private RectTransform _gridTransform;
    private GridData _gridData;
    private ItemInstance _item;
    private CellView _cell;
    private Vector2Int _positionInGrid;
    private Vector2Int _mousePosition;
    private Vector2Int _grabPosition;
    private bool _isPointerClick;
    
    // ---------- Event Method ----------
    
    private void Start()
    {
        CacheComponents();
        SetStartCellImage();
        SetStartItem();
    }

    private void Update()
    {
        _mousePosition = GetGridPoint(Mouse.current.position.ReadValue());
    }

    // ---------- private Method ----------

    private void CacheComponents()
    {
        _gridTransform = GetComponent<RectTransform>();
        _gridData = new GridData(_maxWidth, _maxHeight);
    }

    private void SetStartCellImage()
    {
        for (int i = 0; i < _cells.Length; i++)
        {
            if (i >= 18 && i <= 21)
            {
                _cells[i].SetActive(true);
                continue;
            }

            if (i >= 26 && i <= 29)
            {
                _cells[i].SetActive(true);
                continue;
            }

            _cells[i].SetActive(false);
        }
    }

    private void SetStartItem()
    {
        GameObject tempItem = Instantiate(_itemPrefab, _itemLayer);

        ItemInstance item = tempItem.GetComponent<ItemInstance>();
        item.RectTransform.anchoredPosition = new Vector2(-96, 60);
        
        Vector2Int itemPosition = new();
        itemPosition.x = 2;
        itemPosition.y = 2;
        item.Data.ItemPositionInGrid.Add(itemPosition);
        item.Data.ItemPositionInGrid.Add(new Vector2Int(itemPosition.x, itemPosition.y + 1));
        _gridData.PlaceItem(item, itemPosition);
        
        tempItem = Instantiate(_itemPrefab, _itemLayer);

        item = tempItem.GetComponent<ItemInstance>();
        item.RectTransform.anchoredPosition = new Vector2(32, 60);
        
        itemPosition.x = 4;
        itemPosition.y = 2;
        item.Data.ItemPositionInGrid.Add(itemPosition);
        item.Data.ItemPositionInGrid.Add(new Vector2Int(itemPosition.x, itemPosition.y + 1));
        _gridData.PlaceItem(item, itemPosition);
    }

    private void SetActiveCell(Vector2Int position)
    {
        if (!_gridData.IsInside(position)) return;
        if (_gridData.IsActive(position)) return;

        // TODO 활성화 칸으로 이미지 변경 
        
        _gridData.SetActiveCell(position);
    }

    // ---------- public Method ----------

    public Vector2Int GetGridPoint(Vector2 mousePosition)
    {
        float pointX = (Screen.width / 2) - (_gridTransform.sizeDelta.x / 2) + _gridTransform.anchoredPosition.x;
        float pointY = (Screen.height / 2) + (_gridTransform.sizeDelta.y / 2) + _gridTransform.anchoredPosition.y;
        
        _positionInGrid.x = Mathf.FloorToInt((mousePosition.x - pointX) / CellSize);
        _positionInGrid.y = Mathf.FloorToInt((pointY - mousePosition.y) / CellSize);

        return _positionInGrid; 
    }
    
    public void StartItemDrag()
    {
        _item = _gridData.GetItemAt(_mousePosition);
        _gridData.RemoveItem(_item);
        _item.PrevPosition = _item.RectTransform.anchoredPosition;
        
        _grabPosition = _mousePosition;
        
        for (int i = 0; i < _item.Data.ItemShape.Count; i++)
        {
            Vector2Int temp;        
            temp = _item.Data.ItemPositionInGrid[i] - _grabPosition;
            _item.Data.ItemShape[i] = temp;
        }
    }

    public void EndItemDrag()
    {
        if (_gridData.PlaceItem(_item, _mousePosition))
        {
            for (int i = 0; i < _item.Data.ItemShape.Count; i++)
            {
                Vector2Int temp;
                
                temp = _mousePosition + _item.Data.ItemShape[i];
                _item.Data.ItemPositionInGrid[i] = temp;
            }
        }
        else
        {
            _gridData.RePlaceItem(_item);
            _item.RectTransform.anchoredPosition = _item.PrevPosition;
        }
        
        _item = null;
    }
    
    public void OnPointerUp(PointerEventData eventData)
    {
        
        _isPointerClick = false;
    }
}
