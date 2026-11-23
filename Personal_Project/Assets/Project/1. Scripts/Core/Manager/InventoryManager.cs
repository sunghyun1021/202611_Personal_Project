using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class InventoryManager : SingletonBehaviour<InventoryManager>
{
    [SerializeField] private List<GameObject> _CanActiveImage;
    [SerializeField] private InputActionReference _TestAction;
    [SerializeField] private RectTransform _storageRectTransform;
    
    private Stack<GameObject> _inactiveImageStack = new();
    private Stack<GameObject> _activeImageStack = new();

    public Vector2 StorageLeftTopWorldPosition;
    public Vector2 StorageRightBottomWorldPosition;
    public GridView GridView;
    
    // ---------- Event Method ----------

    private void Awake()
    {
        SetSingleton();
        Init();
    }

    private void OnEnable() => BindTestEvent();
    
    private void OnDisable() => UnBindTestEvent();

    // ---------- private Method ----------

    private void Init()
    {
        foreach (GameObject image in _CanActiveImage)
        {
            image.SetActive(false);
            _inactiveImageStack.Push(image);
        }

        CaculateStorageCorner();
    }
    
    private void BindTestEvent()
    {
        _TestAction.action.Enable();
        _TestAction.action.performed += DisplayerAction;
    }
    
    private void UnBindTestEvent()
    {
        _TestAction.action.Disable();
        _TestAction.action.performed -= DisplayerAction;
    }

    private void DisplayerAction(InputAction.CallbackContext context)
    {
        DisplayCanActiveImage(GridView.FindCanActiveCell());
    }
    
    private void AddDisplayCanActiveImage(Vector2Int position)
    { 
        GameObject canActiveImage = _inactiveImageStack.Pop();
        canActiveImage.SetActive(true);
        canActiveImage.GetComponent<RectTransform>().anchoredPosition =
            new Vector2(position.x * GridView.CellSize,
                -position.y * GridView.CellSize);
        
        _activeImageStack.Push(canActiveImage);
    }

    private void CaculateStorageCorner()
    {
        StorageLeftTopWorldPosition = new Vector2(
            Screen.width / 2 - _storageRectTransform.sizeDelta.x / 2 + _storageRectTransform.anchoredPosition.x,
            Screen.height / 2 + _storageRectTransform.sizeDelta.y / 2 + _storageRectTransform.anchoredPosition.y
        );
        
        StorageRightBottomWorldPosition = new Vector2(
            Screen.width / 2 + _storageRectTransform.sizeDelta.x / 2 + _storageRectTransform.anchoredPosition.x,
            Screen.height / 2 - _storageRectTransform.sizeDelta.y / 2 + _storageRectTransform.anchoredPosition.y
        );
    }
    
    // ---------- public Method ----------

    public void DisplayCanActiveImage(HashSet<Vector2Int> CanActivelist)
    {
        foreach (Vector2Int targetPos in CanActivelist)
        {
            AddDisplayCanActiveImage(targetPos);
        }
    }
    
    public void UnDisplayCanActiveImage()
    {
        foreach (GameObject image in _activeImageStack)
        {
            image.SetActive(false);
            _inactiveImageStack.Push(image);
        }
        _activeImageStack.Clear();
    }
}
