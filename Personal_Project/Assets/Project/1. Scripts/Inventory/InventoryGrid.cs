using System;
using UnityEngine;

public class InventoryGrid : MonoBehaviour
{
    private int _cellSizeX;
    private int _cellSizeY;
    
    private GridLayout _layout;
    private RectTransform _rectTransform;

    public int CellSizeX => _cellSizeX;
    public int CellSizeY => _cellSizeY;

    // ---------- Event Method ----------
    
    private void Awake()
    {
        CacheComponents();
        InitCellSize();
    }
    
    // ----------------------------------
    
    private void CacheComponents()
    {
        _layout = GetComponent<GridLayout>();
        _rectTransform = GetComponent<RectTransform>();
    }
    
    private void InitCellSize()
    {
        _cellSizeX = (int)_layout.cellSize.x;
        _cellSizeY = (int)_layout.cellSize.y;
    }

    private void GetGridZeroPoint()
    {
        
    }

}
