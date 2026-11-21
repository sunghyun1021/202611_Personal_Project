using System.Collections.Generic;
using UnityEngine;

public class ItemInstance : MonoBehaviour
{
    [SerializeField] private ItemSO _itemSO;
    
    public ItemData Data;
    public float PrevRotation;
    [HideInInspector] public Vector2Int PrevPosition; 
    [HideInInspector] public Vector2Int PositionInGrid;
    public RectTransform RectTransform;
    
    // ---------- Event Method ----------

    private void Awake() => CacheComponents();
    
    // ---------- private Method ----------

    private void CacheComponents()
    {
        Data = new  ItemData(_itemSO);
        RectTransform =  GetComponent<RectTransform>();
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
}
