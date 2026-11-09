using System;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float _moveSpeed;
    
    private PlayerInputSystem _playerInput;
    
    // ---------- Event Method ----------

    private void Awake() => CacheComponents();

    private void Update()
    {
        transform.Translate(_playerInput.MoveAxisRaw * (_moveSpeed * Time.deltaTime)); 
    }

    // ----------------------------------

    private void CacheComponents()
    {
        _playerInput = GetComponent<PlayerInputSystem>();
    }
    
    
}
