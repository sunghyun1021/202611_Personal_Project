using UnityEngine;

public class PlayerData : MonoBehaviour
{
    [Header("최대 체력")]
    [SerializeField] private int _maxHealth;
    [Header("현재 체력")]
    [SerializeField] private int _currentHealth;
    [Header("최대 실드량")]
    [SerializeField] private float _maxShield;
    [Header("현재 실드량")]
    [SerializeField] private float _currentShield;
    [Header("이동속도")]
    [SerializeField] private float _moveSpeed;
    [Header("대시 힘")]
    [SerializeField] private float _DashForce;
    [Header("대시 쿨타임")]
    [SerializeField] private float _DashCoolTime;
    
    
    [HideInInspector] public ObservableProperty<int> MaxHealth;
    [HideInInspector] public ObservableProperty<int> CurrentHealth;
    [HideInInspector] public ObservableProperty<float> MaxShield;
    [HideInInspector] public ObservableProperty<float> CurrentShield;
    
    public float MoveSpeed => _moveSpeed;
    public float DashForce => _DashForce;
    public float DashCoolTime => _DashCoolTime;
    
    private void Start() => Init();

    // --------------------

    private void Init()
    {
        CurrentHealth.Value = _maxHealth;
        MaxHealth.Value = _maxHealth;
        CurrentShield.Value = _maxShield;
        MaxShield.Value = _maxShield;
    }
}
