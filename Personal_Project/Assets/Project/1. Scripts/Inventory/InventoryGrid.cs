using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class InventoryGrid : MonoBehaviour, IPointerEnterHandler,IPointerExitHandler
{
    private int _cellSizeX;
    private int _cellSizeY;
    private bool _isPointerInGrid;
    
    private GridLayoutGroup _layout;
    private RectTransform _rectTransform;
    private PlayerInputSystem _playerInputSystem;
    private Vector2Int _PositionInGrid;

    public int CellSizeX => _cellSizeX;
    public int CellSizeY => _cellSizeY;

    // ---------- Event Method ----------
    
    private void Awake()
    {
        CacheComponents();
        InitCellSize();
    }

    private void FixedUpdate()
    {
        if(_isPointerInGrid)
        {
            Debug.Log(GetGridPoint(_playerInputSystem.MousePosition));
        }
    }

    // ----------------------------------
    
    private void CacheComponents()
    {
        _layout = GetComponent<GridLayoutGroup>();
        _rectTransform = GetComponent<RectTransform>();
        _playerInputSystem = GetComponent<PlayerInputSystem>();
    }
    
    private void InitCellSize()
    {
        _cellSizeX = (int)_layout.cellSize.x;
        _cellSizeY = (int)_layout.cellSize.y;
    }
    
    public Vector2Int GetGridPoint(Vector2 mousePosition)
    {
        int pointX = (Screen.width / 2) - ((int)_rectTransform.sizeDelta.x / 2) + (int)_rectTransform.anchoredPosition.x;
        int pointY = (Screen.height / 2) + ((int)_rectTransform.sizeDelta.y / 2) + (int)_rectTransform.anchoredPosition.y;

        _PositionInGrid.x =  ((int)mousePosition.x - pointX) / _cellSizeX; 
        _PositionInGrid.y = -((int)mousePosition.y - pointY) / _cellSizeY; 
        
        return _PositionInGrid;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        Debug.Log("OnPointerEnter");
        _isPointerInGrid = true;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        Debug.Log("OnPointerExit");
        _isPointerInGrid = false;
    }
}
