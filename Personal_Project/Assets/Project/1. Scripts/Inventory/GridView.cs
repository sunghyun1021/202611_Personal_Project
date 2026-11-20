using UnityEngine;

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
    
    // ---------- Event Method ----------
    
    private void Start()
    {
        CacheComponents();
        SetActiveGrid();
    }
    
    // ---------- private Method ----------

    private void CacheComponents()
    {
        _gridTransform = GetComponent<RectTransform>();
        _gridData = new GridData(_maxWidth, _maxHeight);
    }
    
    private void SetActiveGrid()
    {
        
    }
    
    // ---------- private Method ----------
}
