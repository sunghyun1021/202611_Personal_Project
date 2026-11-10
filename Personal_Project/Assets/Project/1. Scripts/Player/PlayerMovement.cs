using System;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float _moveSpeed;
    [SerializeField] private float _DashForce;
    
    private Rigidbody _rigidbody;
    private PlayerInputSystem _playerInput;
    
    // ---------- Event Method ----------

    private void Awake() => CacheComponents();

    private void FixedUpdate()
    {
        Move();
        Dash();
    }

    // ----------------------------------

    private void CacheComponents()
    {
        _playerInput = GetComponent<PlayerInputSystem>();
        _rigidbody = GetComponent<Rigidbody>();
    }

    private void Move()
    {
        transform.Translate(_playerInput.MoveAxisRaw * (_moveSpeed * Time.deltaTime));
    }

    private void Dash()
    {
        if (!_playerInput.isDashed) return;

        _rigidbody.AddForce(transform.forward * _DashForce, ForceMode.Impulse);
        
        _playerInput.isDashed = false;
    }
}
