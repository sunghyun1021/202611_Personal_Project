using System;
using UnityEngine;

public class InventoryManager : SingletonBehaviour<InventoryManager>
{
    public GridView GridView;
    
    // ---------- Event Method ----------

    private void Awake() => SetSingleton();

    // ---------- private Method ----------
}
