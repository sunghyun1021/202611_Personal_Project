using System.Collections;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private Animator _playerAnimator;
    private PlayerData _playerData;
    private Rigidbody _rigidbody;
    private PlayerInputSystem _playerInput;
    private WaitForSeconds _waitForSeconds;

    private bool _canDash;
    private bool _isFacingRight;
    private bool _isDashing;

    public float DepthFromCamera = 10f;
    public Transform AimPoint;
    
    // ---------- Event Method ----------

    private void Awake() => CacheComponents();

    private void Start() => Init();
    
    private void FixedUpdate()
    {
        float flipX = _isFacingRight ? 1 : -1;

        _playerAnimator.SetFloat("Speed", _playerInput.MoveAxisRaw.x * flipX);
        
        Aim();
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
        if (_isDashing) return;

        _rigidbody.linearVelocity = new Vector3(
            _playerInput.MoveAxisRaw.x * _playerData.MoveSpeed, 0f, 0f);
    }

    private void Dash()
    {
        if (!_canDash || !_playerInput.isDashed)
        {
            _playerInput.isDashed = false;
            return;
        }

        _isDashing = true;
        _rigidbody.AddForce(transform.forward * (_playerInput.MoveAxisRaw.x * _playerData.DashForce), ForceMode.Impulse);
        _playerInput.isDashed = false;
        _canDash = false;
        StartCoroutine(MoveInputStop());
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
        Vector3 mouseScreenPos = new Vector3(
            _playerInput.MousePosition.x,
            _playerInput.MousePosition.y,
            DepthFromCamera);

        Vector3 worldPos = Camera.main.ScreenToWorldPoint(mouseScreenPos);
        
        AimPoint.position = worldPos;

        if (worldPos.x < transform.position.x && _isFacingRight)
        {
            Filp();
        }
        else if (worldPos.x > transform.position.x && !_isFacingRight)
        {
            Filp(); 
        }
    }

    private IEnumerator MoveInputStop()
    {
        yield return new WaitForSeconds(0.3f);
        _isDashing = false;
    }
    
    private IEnumerator DashCoolCalculate()
    {
        yield return _waitForSeconds;
        _canDash = true;
    }
}
