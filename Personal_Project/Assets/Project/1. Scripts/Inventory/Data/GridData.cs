using System.Collections.Generic;
using UnityEngine;

public class GridData
{
    private ItemInstance[,] _itemInCell;
    private List<ItemInstance> _items;
    private List<Vector2Int> _ActiveCells;
    private bool[,] _isActive;
    
    public int MaxWidth;
    public int MaxHeight;

    public GridData(int maxWidth, int maxHeight)
    {
        MaxWidth = maxWidth;
        MaxHeight = maxHeight;
        _itemInCell = new ItemInstance[maxWidth, maxHeight];
        _items = new List<ItemInstance>();
        _ActiveCells = new List<Vector2Int>();
        _isActive = new bool[maxWidth, maxHeight];
        
        for (int i = 0; i < maxWidth; i++)
        {
            for (int j = 0; j < maxHeight; j++)
            {
                _itemInCell[i, j] = null;

                // 초기 활성화칸 설정, 초기 아이템 설정
                if (i >= 2 && i <= 5 && j >= 2 && j <= 3)
                {
                    _isActive[i, j] = true;
                    _ActiveCells.Add(new Vector2Int(i, j));
                    continue;
                }
                
                _isActive[i, j] = false;
            }
        }
    }
    
    // ---------- private Method ----------
    
    // 배치할 수 있는가?
    private bool CanPlace(ItemInstance item, Vector2Int target)
    {
        for (int i = 0; i < item.Data.ItemShape.Count; i++)
        {
            Vector2Int targetPos = item.Data.ItemShape[i] + target;
            
            if (GetItemAt(targetPos) != null) return false;
            if (!IsInside(target)) return false;
            if (!IsActive(targetPos)) return false;
        }

        return true;
    }

    // ---------- public Method ----------
    
    // 배열 내에 있는가?
    public bool IsInside(Vector2Int target)
    {
        if (target.x < 0) return false;
        if (target.x >= MaxWidth) return false;
        if (target.y < 0) return false;
        if (target.y >= MaxHeight) return false;

        return true;
    }
    
    // 활성화되어있는가?
    public bool IsActive(Vector2Int target)
    {
        if (!_isActive[target.x, target.y]) return false;

        return true;
    }
    
    public ItemInstance GetItemAt(Vector2Int target)
    {
        if (!IsInside(target)) return null;
        if (!IsActive(target)) return null;
        
        return _itemInCell[target.x, target.y];
    }

    public bool PlaceItem(ItemInstance item, Vector2Int target)
    {
        Debug.Log($"target : {target.x}, {target.y}");
        if (!CanPlace(item, target))
        {
            Debug.Log("불가능");
            return false;
        }
        Debug.Log("가능");
        for (int i = 0; i < item.Data.ItemShape.Count; i++)
        {
            Vector2Int targetPos = item.Data.ItemShape[i] + target;
            
            _itemInCell[targetPos.x, targetPos.y] = item;
        }
        
        _items.Add(item);
        return true;
    }
    
    public void RePlaceItem(ItemInstance item)
    {
        for (int i = 0; i < item.Data.ItemShape.Count; i++)
        {
            Vector2Int targetPos = item.Data.ItemPositionInGrid[i];
            
            _itemInCell[targetPos.x, targetPos.y] = item;
        }
        
        _items.Add(item);
    }
    
    public void RemoveItem(ItemInstance item)
    {
        for (int i = 0; i < item.Data.ItemShape.Count; i++)
        {
            Vector2Int targetPos = item.Data.ItemPositionInGrid[i];
            
            _itemInCell[targetPos.x, targetPos.y] = null;
            
            Debug.Log($"할당 해제 위치 : {targetPos.x}, {targetPos.y}");
        }
        
        _items.Remove(item);
    }
    
    public void SetActiveCell(Vector2Int target)
    {
        if (_isActive[target.x, target.y]) return;
        
        _isActive[target.x, target.y] = true;
        _ActiveCells.Add(target);
        Debug.Log($"{target.x}, {target.y} 활성화됨");
    }

    public bool CheckActiveCell4Direction(Vector2Int target)
    {
        Vector2Int addPosition = new Vector2Int(1, 0);
        
        if (!IsActive(target + addPosition)) addPosition = new Vector2Int(0, 1);
        else return true;
        
        if (!IsActive(target + addPosition)) addPosition = new Vector2Int(-1, 0);
        else return true;

        if (!IsActive(target + addPosition)) addPosition = new Vector2Int(0, -1);
        else return true;

        return false;
    }
} 
