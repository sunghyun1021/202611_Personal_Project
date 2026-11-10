using System;
using System.Collections;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private PlayerData _playerData;
    private Rigidbody _rigidbody;
    private PlayerInputSystem _playerInput;
    private WaitForSeconds _waitForSeconds;

    private bool _canDash;
    
    // ---------- Event Method ----------

    private void Awake() => CacheComponents();

    private void Start() => Init();
    
    private void FixedUpdate()
    {
        Move();
        Dash();
    }

    // ----------------------------------

    private void CacheComponents()
    {
        _playerData = GetComponent<PlayerData>();
        _playerInput = GetComponent<PlayerInputSystem>();
        _rigidbody = GetComponent<Rigidbody>();
    }

    private void Init()
    {
        _waitForSeconds = new WaitForSeconds(_playerData.DashCoolTime);
        _canDash = true;
    }

    private void Move()
    {
        transform.Translate(_playerInput.MoveAxisRaw * (_playerData.MoveSpeed * Time.deltaTime));
    }

    private void Dash()
    {
        if (!_canDash || !_playerInput.isDashed)
        {
            _playerInput.isDashed = false;
            return;
        }
        
        _rigidbody.AddForce(transform.forward * _playerData.DashForce, ForceMode.Impulse);
        _playerInput.isDashed = false;
        _canDash = false;
        StartCoroutine(DashCoolCalculate());
    }

    private IEnumerator DashCoolCalculate()
    {
        yield return _waitForSeconds;
        _canDash = true;
    }
}
