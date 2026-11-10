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
        Move();
    }

    // ----------------------------------

    private void CacheComponents()
    {
        _playerInput = GetComponent<PlayerInputSystem>();
    }

    private void Move()
    {
        transform.Translate(_playerInput.MoveAxisRaw * (_moveSpeed * Time.deltaTime));
    }
    
    
}
