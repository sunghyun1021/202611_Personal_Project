using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class InventoryManager : SingletonBehaviour<InventoryManager>
{
    [SerializeField] private List<GameObject> _CanActiveImage;
    
    private PlayerInputSystem _playerInputSystem;
    
    public GridView GridView;
    
    // ---------- Event Method ----------

    private void Awake()
    {
        _playerInputSystem = GetComponent<PlayerInputSystem>();
        SetSingleton();
    }

    private void OnEnable() => BindTestEvent();
    
    private void OnDisable() => UnBindTestEvent();

    // ---------- private Method ----------

    private void BindTestEvent()
    {
        _playerInputSystem.OnTestKey.performed += DisplayerAction;
    }
    
    private void UnBindTestEvent()
    {
        _playerInputSystem.OnTestKey.performed -= DisplayerAction;
    }

    private void DisplayerAction(InputAction.CallbackContext context)
    {
        Debug.Log("시작됨@@@@@@@");
        DisplayCanActiveImage(GridView.FindCanActiveCell());
    }
    
    private void DisplayCanActiveImage(List<Vector2Int> CanActivelist)
    {
        for (int i = 0; i < CanActivelist.Count; i++)
        {
            _CanActiveImage[i].SetActive(true);
            _CanActiveImage[i].transform.position = 
                new Vector2(0 * GridView.CellSize,
                    0 * GridView.CellSize);
        }
    }
    
    private void UnDisplayCanActiveImage()
    {
        for (int i = 0; i < _CanActiveImage.Count; i++)
        {
            _CanActiveImage[i].SetActive(false);
        }
    }
}
