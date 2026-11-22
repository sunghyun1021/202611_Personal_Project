using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class InventoryManager : SingletonBehaviour<InventoryManager>
{
    [SerializeField] private List<GameObject> _CanActiveImage;
    [SerializeField] private InputActionReference _TestAction;
    
    public GridView GridView;
    
    // ---------- Event Method ----------

    private void Awake()
    {
        SetSingleton();
    }

    private void OnEnable() => BindTestEvent();
    
    private void OnDisable() => UnBindTestEvent();

    // ---------- private Method ----------

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
    
    private void DisplayCanActiveImage(HashSet<Vector2Int> CanActivelist)
    {
        Debug.Log($"_CanActiveImage.Count : {_CanActiveImage.Count}");

        int count = 0;
        foreach (Vector2Int targetPos in CanActivelist)
        {
            _CanActiveImage[count].gameObject.SetActive(true);
            _CanActiveImage[count].GetComponent<RectTransform>().anchoredPosition =
                new Vector2(targetPos.x * GridView.CellSize,
                    -targetPos.y * GridView.CellSize);
            
            count++;
        }
    }
    
    private void UnDisplayCanActiveImage()
    {
        for (int i = 0; i < _CanActiveImage.Count; i++)
        {
            _CanActiveImage[i].gameObject.SetActive(false);
        }
    }
}
