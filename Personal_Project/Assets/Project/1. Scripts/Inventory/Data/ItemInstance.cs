using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

public class ItemInstance : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    [SerializeField] private ItemSO _itemSO;
    [SerializeField] private GridView _gridView;
    [SerializeField] private InputActionReference _rotateAction;

    public List<Vector2Int> RelativeCoords = new();
    public List<Vector2Int> ItemPositionInGrid = new();

    private Vector2 _currentMousePosition;
    private Vector2 _mouseOffset;
    private Vector2 _selectedImageCellCenterOffset;
    private Vector2 _initialPosition;
    private Vector3 _initialEulerAngles;
    private bool _isDragging;
    private int _cellSize;

    public ItemData Data;
    [HideInInspector] public Vector2 PrevPosition;
    [HideInInspector] public float PrevRotation;
    [HideInInspector] public Vector2Int PositionInGrid;
    [HideInInspector] public RectTransform RectTransform;


    // ---------- Event Method ----------

    private void Awake() => CacheComponents();
    private void Start() => Init();
    private void OnEnable() => BindInputActions();
    private void OnDisable() => UnbindInputActions();

    // ---------- private Method ----------

    private void CacheComponents()
    {
        Data = new ItemData(_itemSO);
        RectTransform = GetComponent<RectTransform>();
        _gridView = InventoryManager.Instance.GridView;
    }

    private void Init()
    {
        _cellSize = GridView.CellSize;
    }

    private void BindInputActions()
    {
        _rotateAction.action.Enable();
        _rotateAction.action.performed += Rotate;
    }

    private void UnbindInputActions()
    {
        _rotateAction.action.Disable();
        _rotateAction.action.performed -= Rotate;
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        _isDragging = true;
        _initialPosition = RectTransform.position;
        _initialEulerAngles = RectTransform.eulerAngles;

        _gridView.StartItemDrag();

        int rotation = Mathf.RoundToInt(RectTransform.eulerAngles.z / 90);
        bool isSizeDeltaRotated = rotation % 2 != 0;

        float width = isSizeDeltaRotated ? RectTransform.sizeDelta.y : RectTransform.sizeDelta.x;
        float height = isSizeDeltaRotated ? RectTransform.sizeDelta.x : RectTransform.sizeDelta.y;

        int widthSize = Mathf.FloorToInt(width / _cellSize);
        int heightSize = Mathf.FloorToInt(height / _cellSize);

        float leftTopPosX = RectTransform.position.x - width / 2;
        float leftTopPosY = RectTransform.position.y + height / 2;

        Vector2 leftTopPosition = new Vector2(leftTopPosX, leftTopPosY);

        Vector2Int mouseCoord = new Vector2Int(
            Mathf.FloorToInt(eventData.pressPosition.x - leftTopPosition.x) / _cellSize
            , Mathf.FloorToInt((leftTopPosition.y - eventData.pressPosition.y) / _cellSize));

        RelativeCoords.Clear();

        for (int j = 0; j < heightSize; j++)
        {
            for (int i = 0; i < widthSize; i++)
            {
                RelativeCoords.Add(new Vector2Int(i, j));
            }
        }
        
        for (int i = 0; i < RelativeCoords.Count; i++)
        {
            RelativeCoords[i] = RelativeCoords[i] - mouseCoord;
        }

        Vector2 _selectedImageCellCenter = new Vector2(
            leftTopPosition.x + mouseCoord.x * _cellSize + _cellSize / 2,
            leftTopPosition.y - mouseCoord.y * _cellSize - _cellSize / 2
        );

        _mouseOffset = new Vector2(
            eventData.pressPosition.x - RectTransform.position.x,
            eventData.pressPosition.y - RectTransform.position.y
        );

        _selectedImageCellCenterOffset = new Vector2(
            _selectedImageCellCenter.x - RectTransform.position.x,
            _selectedImageCellCenter.y - RectTransform.position.y
        );
    }


    public void OnDrag(PointerEventData eventData)
    {
        float posX = eventData.position.x - _mouseOffset.x;
        float posY = eventData.position.y - _mouseOffset.y;

        _currentMousePosition = eventData.position;

        RectTransform.position = new Vector3(posX, posY, 0);
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        _isDragging = false;
        
        if (_gridView.CanPlaceItem())
        {
            Debug.Log("배치 가능");
            _gridView.PlaceItem();
            PlaceItem(eventData.position);
        }
        else
        {
            Debug.Log("배치 불가능");
            _gridView.RePlaceItem();
            RectTransform.position = _initialPosition;
            RectTransform.eulerAngles = _initialEulerAngles;
        }
    }

    private void PlaceItem(Vector2 mousePosition)
    {
        Vector2 leftTopGridPosition = new Vector2(
            Screen.width / 2 - _gridView.GridTransform.sizeDelta.x / 2 + _gridView.GridTransform.anchoredPosition.x,
            Screen.height / 2 + _gridView.GridTransform.sizeDelta.y / 2 + _gridView.GridTransform.anchoredPosition.y
        );
        Debug.Log($"leftTopGridPosition : {leftTopGridPosition}");
        
        float posX = leftTopGridPosition.x + _cellSize * _gridView.GetGridPoint(mousePosition).x + _cellSize / 2;
        float posY = leftTopGridPosition.y - _cellSize * _gridView.GetGridPoint(mousePosition).y - _cellSize / 2;

        RectTransform.position = new Vector3(
            posX - _selectedImageCellCenterOffset.x,
            posY - _selectedImageCellCenterOffset.y, 
            0
        );
        Debug.Log($"RectTransform.position : {RectTransform.position}");
    }

private void Rotate(InputAction.CallbackContext context)
    {
        if (!_isDragging) return;

        RectTransform.Rotate(new Vector3(0f, 0f, -90f));

        _mouseOffset = new Vector3(_mouseOffset.y, - _mouseOffset.x, 0f);
        
        _selectedImageCellCenterOffset = new Vector2(
            _selectedImageCellCenterOffset.y,
            -_selectedImageCellCenterOffset.x
            );

        float posX = _currentMousePosition.x - _mouseOffset.x;
        float posY = _currentMousePosition.y - _mouseOffset.y;
        RectTransform.position = new Vector3(posX, posY, 0);

        for (int i = 0; i < RelativeCoords.Count; i++)
        {
            RelativeCoords[i] = new Vector2Int(-RelativeCoords[i].y, RelativeCoords[i].x);
        }
    }
}
