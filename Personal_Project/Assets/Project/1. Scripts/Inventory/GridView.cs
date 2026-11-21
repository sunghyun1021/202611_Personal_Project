using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

public class GridView : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public const int CellSize = 64;
    
    [SerializeField] private int _maxWidth;
    [SerializeField] private int _maxHeight;
    [SerializeField] private RectTransform _cellLayer;
    [SerializeField] private RectTransform _itemLayer;
    [SerializeField] private CellView[] _cells;
    
    private RectTransform _gridTransform;
    private GridData _gridData;
    private Vector2Int _positionInGrid;
    private bool _isPointerEnter;
    
    // ---------- Event Method ----------
    
    private void Start()
    {
        CacheComponents();

    }

    private void Update()
    {
        if(_isPointerEnter)
        {
            if(Mouse.current.leftButton.wasPressedThisFrame)
            {
                Debug.Log(GetGridPoint(Mouse.current.position.ReadValue()));
            }
        }
    }

    // ---------- private Method ----------

    private void CacheComponents()
    {
        _gridTransform = GetComponent<RectTransform>();
        _gridData = new GridData(_maxWidth, _maxHeight);
    }
    
    // ---------- public Method ----------
    
    public Vector2Int GetGridPoint(Vector2 mousePosition)
    {
        float pointX = (Screen.width / 2) - (_gridTransform.sizeDelta.x / 2) + _gridTransform.anchoredPosition.x;
        float pointY = (Screen.height / 2) + (_gridTransform.sizeDelta.y / 2) + _gridTransform.anchoredPosition.y;

        // TODO Debug Delete
        //Debug.Log($"PointX: {pointX}, PointY: {pointY}");
        //Debug.Log($"mousePosition: {mousePosition}");
        
        _positionInGrid.x = Mathf.FloorToInt((mousePosition.x - pointX) / CellSize); 
        _positionInGrid.y = Mathf.FloorToInt((pointY - mousePosition.y) / CellSize);

        return _positionInGrid;
        GridLayout gird;
    }
    
    public void OnPointerEnter(PointerEventData eventData)
    {
        _isPointerEnter = true;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        _isPointerEnter = false;
    }
}
