using System.Collections;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private PlayerData _playerData;
    private Rigidbody _rigidbody;
    private PlayerInputSystem _playerInput;
    private WaitForSeconds _waitForSeconds;

    private bool _canDash;
    private bool _isFacingRight;
    
    // ---------- Event Method ----------

    private void Awake() => CacheComponents();

    private void Start() => Init();
    
    private void FixedUpdate()
    {
        if (_playerInput.MoveAxisRaw.x < 0 && _isFacingRight)
        {
            Filp();
        }
        else if (_playerInput.MoveAxisRaw.x > 0 && !_isFacingRight)
        {
            Filp();
        }
        
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
        _isFacingRight = true;
    }

    private void Move()
    {
        if (_playerInput.MoveAxisRaw.x == 0) return;
        
        transform.Translate(transform.forward * (_playerData.MoveSpeed * Time.deltaTime), Space.World);
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

    private void Filp()
    {
        if (_isFacingRight)
        {
            transform.localRotation = Quaternion.Euler(0f, -90f, 0f);
            _isFacingRight = false;
        }
        else
        {
            transform.localRotation = Quaternion.Euler(0f, 90f, 0f);
            _isFacingRight = true;
        }
    }

    private void Aim()
    {
        Vector3 mouseScreenPos = Input.mousePosition;
        
    }

    private IEnumerator DashCoolCalculate()
    {
        yield return _waitForSeconds;
        _canDash = true;
    }
}
