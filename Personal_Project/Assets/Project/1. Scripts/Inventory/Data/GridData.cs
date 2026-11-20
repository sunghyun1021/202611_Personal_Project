using System.Collections.Generic;
using UnityEngine;

public class GridData
{
    private int _maxWidth;
    private int _maxHeight;

    private bool[,] _isActive;
    private ItemInstance[,] _cells;
    private List<ItemInstance> _items;

    private GridData(int maxWidth, int maxHeight)
    {
        _maxWidth = maxWidth;
        _maxHeight = maxHeight;
        _isActive[_maxWidth, _maxHeight] = true;
        _cells = new ItemInstance[_maxWidth, _maxHeight];
        _items = new List<ItemInstance>();
    }
    
    // ---------- private Method ----------
    
    private bool IsInside(Vector2Int target)
    {
        if (target.x < 0) return false;
        if (target.x > _maxWidth) return false;
        if (target.y < 0) return false;
        if (target.y > _maxHeight) return false;

        return true;
    }

    private bool IsActive(Vector2Int target)
    {
        if (!_isActive[target.x, target.y]) return false;

        return true;
    }
    
    private bool CanPlace(ItemInstance item, Vector2Int target)
    {
        for (int i = 0; i < item.Data.ItemShape.Count; i++)
        {
            Vector2Int targetPos = item.Data.ItemShape[i] + target;
            
            if (GetItemAt(targetPos) != null)
            {
                return false;
            }
        }

        return true;
    }
    
    private void RemoveItem(ItemInstance item)
    {
        for (int i = 0; i < item.Data.ItemShape.Count; i++)
        {
            Vector2Int targetPos = item.PositionInGrid + item.Data.ItemShape[i];
            
            _cells[targetPos.x, targetPos.y] = null;
        }
        
        _items.Remove(item);
    }
    
    private void RePlaceItem(ItemInstance item, Vector2Int target)
    {
        for (int i = 0; i < item.Data.ItemShape.Count; i++)
        {
            Vector2Int targetPos = item.Data.ItemShape[i] + target;
            
            _cells[targetPos.x, targetPos.y] = item;
        }
        
        _items.Add(item);
    }
    
    // ---------- public Method ----------
    
    
    private ItemInstance GetItemAt(Vector2Int target)
    {
        if (!IsInside(target)) return null;
        if (!IsActive(target)) return null;
        
        return _cells[target.x, target.y];
    }

    public void PlaceItem(ItemInstance item, Vector2Int target)
    {
        if (!CanPlace(item, target))
        {
            RePlaceItem(item, item.PrevPosition);
            return;
        }

        for (int i = 0; i < item.Data.ItemShape.Count; i++)
        {
            Vector2Int targetPos = item.Data.ItemShape[i] + target;
            
            _cells[targetPos.x, targetPos.y] = item;
        }
        
        _items.Add(item);
    }
    
    public void SetActiveCell(Vector2Int target)
    {
        if (_isActive[target.x, target.y]) return;
        
        _isActive[target.x, target.y] = true;
        // TODO 액티브상태 이미지로 변경
    }
} 
