using System;
using System.Collections;
using UnityEngine;

public class PlayerController : MonoBehaviour, IDamageable
{
    [SerializeField] private Animator _playerAnimator;
    [SerializeField] private Transform _muzzlePoint;
    [SerializeField] private float _invincibleTime;
    
    // TODO 밑에 항목들은 임시용
    [SerializeField] private GameObject _projectilePrefab;
    [SerializeField] private float _shootCoolTime;
    
    private PlayerMovement _playerMovement;
    private PlayerData _playerData;
    private PlayerInputSystem _playerInputSystem;
    private WaitForSeconds _waitForShoot;
    private WaitForSeconds _waitForInvincibleTime;
    private Collider _collider;
    private int _remainDamage;
    private float _shieldFillUpStartTime;
    private bool _isCool;
    private bool _canHit;
    
    public event Action OnPlayerDead;
    public GameObject GameObject { get => gameObject; }
    public Transform Transform { get => transform; }
    public Animator PlayerAnimator => _playerAnimator;
    
    // ---------- Event Method ----------
    
    private void Awake() => CacheComponents();

    private void Start() => Init();
    
    private void Update()
    {
        if (_playerInputSystem.IsShootKeyPressed && !_isCool)
        {
            Shoot();
        }

        if (Time.time >= _shieldFillUpStartTime)
        {
            ShieldFillUp();
        }
        
        if (_playerInputSystem.IsTestKeyPressed)
        {
            
            foreach (ItemInstance itemInstance in FlowManager.Instance.StorageItemList)
            {
                Debug.Log("들어옴");
                Debug.Log(itemInstance);
            }
            
            _playerInputSystem.IsTestKeyPressed = false;
        }
    }

    // ---------- private Method ----------

    private void CacheComponents()
    {
        _playerData = GetComponent<PlayerData>();
        _playerMovement = GetComponent<PlayerMovement>();
        _playerInputSystem = GetComponent<PlayerInputSystem>();
        _collider = GetComponent<Collider>();

        GameManager.Instance.Player = this;
    }

    private void Init()
    {
        _waitForShoot =  new WaitForSeconds(_shootCoolTime);
        _waitForInvincibleTime = new WaitForSeconds(_invincibleTime);
        _canHit = true;
        _collider.enabled = true;
    }

    private void Shoot()
    {
        _isCool = true;
        
        // TODO 임시 테스트용 발사
        GameObject obj = Instantiate(_projectilePrefab, _muzzlePoint.position, _muzzlePoint.rotation);

        if (obj.TryGetComponent(out Projectile projectile))
        {
            projectile.Damage = 10;
        }
        StartCoroutine(CarculateShootTime());
    }

    private IEnumerator CarculateShootTime()
    {
        yield return _waitForShoot;
        _isCool = false;
    }

    private IEnumerator CarculateInvincibleTime()
    {
        yield return _waitForInvincibleTime;
        _canHit = true;
    }
    
    public void TakeDamage(int damage)
    {
        if (_canHit)
        {
            _shieldFillUpStartTime = Time.time + 5f;
            
            if (_playerData.CurrentShield.Value > 0)
            {
                if (_playerData.CurrentShield.Value - damage < 0)
                {
                    _remainDamage = damage - Mathf.FloorToInt(_playerData.CurrentShield.Value);
                    _playerData.CurrentShield.Value = 0;
                    _playerData.CurrentHealth.Value -= _remainDamage;
                }
                else
                {
                    _playerData.CurrentShield.Value -= damage;
                }
            }
            else
            {
                _playerData.CurrentHealth.Value -= damage;
            }

            if (_playerData.CurrentHealth.Value <= 0)
            {
                _playerAnimator.SetTrigger("Dead");
                _collider.enabled = false;
                OnPlayerDead?.Invoke();
            }
            else
            {
                _canHit = false;
                StartCoroutine(CarculateInvincibleTime());
            }
        }
    }

    private void ShieldFillUp()
    {
        if(_playerData.CurrentShield.Value <= _playerData.MaxShield.Value)
        {
            _playerData.CurrentShield.Value += Time.deltaTime * 10;
        } 
    }
}
