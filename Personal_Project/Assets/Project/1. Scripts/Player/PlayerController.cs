using System;
using System.Collections;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private Animator _playerAnimator;
    [SerializeField] private Transform _muzzlePoint;
    [SerializeField] private GameObject _projectilePrefab;
    [SerializeField] private float _shootCoolTime;
    
    private PlayerMovement _playerMovement;
    private PlayerData _playerData;
    private PlayerInputSystem _playerInputSystem;
    private WaitForSeconds _waitForSeconds;
    private bool _isCool;
    
    // ---------- Event Method ----------
    
    private void Awake() => CacheComponents();

    private void Start() => Init();
    
    private void Update()
    {
        if (_playerInputSystem.IsShoot && !_isCool)
        {
            Shoot();
        }
    }

    // ----------------------------------
    
    private void CacheComponents()
    {
        _playerData = GetComponent<PlayerData>();
        _playerMovement = GetComponent<PlayerMovement>();
        _playerInputSystem = GetComponent<PlayerInputSystem>();
    }
    
    private void Init()
    {
        _waitForSeconds =  new WaitForSeconds(_shootCoolTime);
    }

    private void Shoot()
    {
        _isCool = true;
        Instantiate(_projectilePrefab, _muzzlePoint.position, _muzzlePoint.rotation);
        StartCoroutine(CarculateShootTime());
    }

    private IEnumerator CarculateShootTime()
    {
        yield return _waitForSeconds;
        _isCool = false;
    }
}
