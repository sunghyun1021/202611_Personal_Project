using System;
using System.Collections.Generic;
using UnityEngine;

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

    // ---------- private Method ----------
    
    private void DisplayCanActiveImage(List<Vector2Int> CanActivelist)
    {
        
    }
}
