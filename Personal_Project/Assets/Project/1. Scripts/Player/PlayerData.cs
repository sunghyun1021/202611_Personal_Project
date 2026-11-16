using UnityEngine;

public class PlayerData : MonoBehaviour
{
    [Header("이동속도")]
    [SerializeField] private float _moveSpeed;
    [Header("대시 힘")]
    [SerializeField] private float _DashForce;
    [Header("대시 쿨타임")]
    [SerializeField] private float _DashCoolTime;
    
    [Header("최대 체력")]
    public ObservableProperty<int> MaxHealth;
    [Header("현재 체력")]
    public ObservableProperty<int> CurrentHealth;
    [Header("최대 실드량")]
    public ObservableProperty<int> MaxShield;
    [Header("현재 실드량")]
    public ObservableProperty<int> CurrentShield;
    
    public float MoveSpeed => _moveSpeed;
    public float DashForce => _DashForce;
    public float DashCoolTime => _DashCoolTime;
}
