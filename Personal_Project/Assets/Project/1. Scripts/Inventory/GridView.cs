using System;
using System.Collections.Generic;
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

    private Dictionary<Vector2Int, CellView> _cellDic = new();
    private GridData _gridData;
    private ItemInstance _item;
    private CellView _cell;
    private Vector2Int _positionInGrid;
    private Vector2Int _mousePosition;
    private Vector2Int _grabPosition;
    private bool _isPointerClick;
    
    public RectTransform GridTransform;
    public GridData GridData => _gridData;
    
    // ---------- Event Method ----------
    
    private void Start()
    {
        CacheComponents();
        SetStartCellImage();
        SetStartItem();
        CreateDictionary();
    }

    private void Update()
    {
        _mousePosition = GetGridPoint(Mouse.current.position.ReadValue());

        if (Mouse.current.leftButton.isPressed)
        {
            SetActiveCell(_mousePosition);
        }
    }

    // ---------- private Method ----------

    private void CacheComponents()
    {
        GridTransform = GetComponent<RectTransform>();
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
        item.ItemPositionInGrid.Add(itemPosition);
        item.ItemPositionInGrid.Add(new Vector2Int(itemPosition.x, itemPosition.y + 1));
        _gridData.PlaceItem(item, itemPosition);
        
        tempItem = Instantiate(_itemPrefab, _itemLayer);

        item = tempItem.GetComponent<ItemInstance>();
        item.RectTransform.anchoredPosition = new Vector2(32, 60);
        
        itemPosition.x = 4;
        itemPosition.y = 2;
        item.ItemPositionInGrid.Add(itemPosition);
        item.ItemPositionInGrid.Add(new Vector2Int(itemPosition.x, itemPosition.y + 1));
        _gridData.PlaceItem(item, itemPosition);
    }

    private void CreateDictionary()
    {
        for (int i = 0; i < _cells.Length; i++)
        {
            int y = i / _maxWidth;
            int x = i % _maxWidth;
            
            _cellDic[new Vector2Int(x, y)] = _cells[i];
        }
    }

    private void SetActiveCell(Vector2Int position)
    {
        if (!_gridData.IsInside(position)) return;
        if (_gridData.IsActive(position)) return;
        if(!_gridData.CanActiveCells.Contains(position)) return;
        
        // TODO 활성화 칸으로 이미지 변경 
        _cellDic[position].SetActive(true);
        _gridData.CanActiveCells.Remove(position);

        _gridData.SetActiveCell(position);
        
        InventoryManager.Instance.UnDisplayCanActiveImage();
        
        _gridData.CheckCanActiveCell4Direction(position);
        
        InventoryManager.Instance.DisplayCanActiveImage(_gridData.CanActiveCells);
    }

    // ---------- public Method ----------

    public Vector2Int GetGridPoint(Vector2 mousePosition)
    {
        float pointX = (Screen.width / 2) - (GridTransform.sizeDelta.x / 2) + GridTransform.anchoredPosition.x;
        float pointY = (Screen.height / 2) + (GridTransform.sizeDelta.y / 2) + GridTransform.anchoredPosition.y;
        
        _positionInGrid.x = Mathf.FloorToInt((mousePosition.x - pointX) / CellSize);
        _positionInGrid.y = Mathf.FloorToInt((pointY - mousePosition.y) / CellSize);

        return _positionInGrid; 
    }
    
    public void StartItemDrag()
    {
        _item = _gridData.GetItemAt(_mousePosition);
        Debug.Log(_item);
        _gridData.RemoveItem(_item);
    }

    public void PlaceItem()
    {
        for (int i = 0; i < _item.RelativeCoords.Count; i++)
        {
            Vector2Int temp;
            
            temp = _mousePosition + _item.RelativeCoords[i];
            _item.ItemPositionInGrid[i] = temp;
        }
    }

    public void RePlaceItem()
    {
        _gridData.RePlaceItem(_item);
    }

    public bool CanPlaceItem()
    {
        return _gridData.PlaceItem(_item, _mousePosition);
    }

    public HashSet<Vector2Int> FindCanActiveCell()
    {
        _gridData.CheckCanActive(_gridData.ActiveCells);

        return _gridData.CanActiveCells;
    }
}
