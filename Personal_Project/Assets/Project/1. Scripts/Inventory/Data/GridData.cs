using System.Collections.Generic;
using UnityEngine;

public class GridData
{
    private ItemInstance[,] _itemInCell;
    private List<ItemInstance> _items;
    private bool[,] _isActive;
    
    public int MaxWidth;
    public int MaxHeight;

    public GridData(int maxWidth, int maxHeight)
    {
        MaxWidth = maxWidth;
        MaxHeight = maxHeight;
        _itemInCell = new ItemInstance[maxWidth, maxHeight];
        _items = new List<ItemInstance>();
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
                    Debug.Log($"활성화 칸 : {i}, {j}");
                    continue;
                }
                
                _isActive[i, j] = false;
                Debug.Log($"비활성화 칸 : {i}, {j}");
            }
        }
        _items = new List<ItemInstance>();
    }
    
    // ---------- private Method ----------
    
    // 배열 내에 있는가?
    private bool IsInside(Vector2Int target)
    {
        if (target.x < 0) return false;
        if (target.x > MaxWidth) return false;
        if (target.y < 0) return false;
        if (target.y > MaxHeight) return false;

        return true;
    }
    
    // 배치할 수 있는가?
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
    
    private void RePlaceItem(ItemInstance item, Vector2Int target)
    {
        for (int i = 0; i < item.Data.ItemShape.Count; i++)
        {
            Vector2Int targetPos = item.Data.ItemShape[i] + target;
            
            _itemInCell[targetPos.x, targetPos.y] = item;
        }
        
        _items.Add(item);
    }
    
    // ---------- public Method ----------
    
    // 활성화되어있는가?
    public bool IsActive(Vector2Int target)
    {
        if (!IsInside(target)) return false;
        if (!_isActive[target.x, target.y]) return false;

        return true;
    }
    
    public ItemInstance GetItemAt(Vector2Int target)
    {
        if (!IsInside(target)) return null;
        if (!IsActive(target)) return null;
        
        return _itemInCell[target.x, target.y];
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
            
            _itemInCell[targetPos.x, targetPos.y] = item;
        }
        
        _items.Add(item);
    }
    
    public void RemoveItem(ItemInstance item)
    {
        for (int i = 0; i < item.Data.ItemShape.Count; i++)
        {
            Vector2Int targetPos = item.PositionInGrid + item.Data.ItemShape[i];
            
            _itemInCell[targetPos.x, targetPos.y] = null;
        }
        
        _items.Remove(item);
    }
    
    public void SetActiveCell(Vector2Int target)
    {
        if (_isActive[target.x, target.y]) return;
        
        _isActive[target.x, target.y] = true;
    }
} 
