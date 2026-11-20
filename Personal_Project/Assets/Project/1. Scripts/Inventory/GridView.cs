using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

public class GridView : MonoBehaviour
{
    public const int CellSize = 64;
    
    [SerializeField] private int _maxWidth;
    [SerializeField] private int _maxHeight;
    [SerializeField] private CellView _cell;
    [SerializeField] private RectTransform _cellLayer;
    [SerializeField] private RectTransform _itemLayer;
    
    private RectTransform _gridTransform;
    private GridData _gridData;
    private Vector2Int _PositionInGrid;
    
    // ---------- Event Method ----------
    
    private void Start()
    {
        CacheComponents();
        SetActiveGrid();
    }

    private void Update()
    {
        Debug.Log(GetGridPoint(Mouse.current.position.ReadValue()));
    }

    // ---------- private Method ----------

    private void CacheComponents()
    {
        _gridTransform = GetComponent<RectTransform>();
        _gridData = new GridData(_maxWidth, _maxHeight);
    }
    
    public Vector2Int GetGridPoint(Vector2 mousePosition)
    {
        float pointX = (Screen.width / 2) - (_gridTransform.sizeDelta.x / 2) + _gridTransform.anchoredPosition.x;
        float pointY = (Screen.height / 2) + (_gridTransform.sizeDelta.y / 2) + _gridTransform.anchoredPosition.y;

        Debug.Log($"PointX: {pointX}, PointY: {pointY}");
        Debug.Log($"mousePosition: {mousePosition}");
        
        _PositionInGrid.x = Mathf.FloorToInt((mousePosition.x - pointX) / CellSize); 
        _PositionInGrid.y = Mathf.FloorToInt((pointY - mousePosition.y) / CellSize);

        return _PositionInGrid;
    }
    
    private void SetActiveGrid()
    {
        
    }
    
    // ---------- private Method ----------
}
