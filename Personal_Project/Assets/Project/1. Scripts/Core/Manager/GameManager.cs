using System;
using UnityEngine;

public class GameManager : SingletonBehaviour<GameManager>
{
    public PlayerController Player;
    
    // ---------- Event Method ----------

    private void Awake() => SetSingleton();
    
    // ----------------------------------

    
}
