using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class InventoryGrid : MonoBehaviour, IPointerEnterHandler,IPointerExitHandler
{
    [SerializeField] private Vector2Int GridSize = new();
    
    private bool _isPointerInGrid;
    
    private GridLayoutGroup _layout;
    private RectTransform _rectTransform;
    private PlayerInputSystem _playerInputSystem;
    private Vector2Int _PositionInGrid;
    
    public Vector2Int CellSize { get; private set; }

    // ---------- Event Method ----------
    
    private void Awake()
    {
        CacheComponents();
        InitCellSize();
        SetRectSize();
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
        CellSize = new Vector2Int((int)_layout.cellSize.x, (int)_layout.cellSize.y);
    }

    private void SetRectSize()
    {
        int width = CellSize.x * GridSize.x;
        int height = CellSize.y * GridSize.y;
        
        _rectTransform.sizeDelta = new Vector2(width, height);
    }
    
    public Vector2Int GetGridPoint(Vector2 mousePosition)
    {
        int pointX = (Screen.width / 2) - ((int)_rectTransform.sizeDelta.x / 2) + (int)_rectTransform.anchoredPosition.x;
        int pointY = (Screen.height / 2) + ((int)_rectTransform.sizeDelta.y / 2) + (int)_rectTransform.anchoredPosition.y;

        _PositionInGrid.x =  ((int)mousePosition.x - pointX) / CellSize.x; 
        _PositionInGrid.y = -((int)mousePosition.y - pointY) / CellSize.y; 
        
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
