using UnityEngine;

public class PlayerData : MonoBehaviour
{
    [SerializeField] private int _maxHp;
    [SerializeField] private int _maxShield;
    [SerializeField] private float _moveSpeed;
    [SerializeField] private float _DashForce;
    [SerializeField] private float _DashCoolTime;
    
    public float MoveSpeed => _moveSpeed;
    public float DashForce => _DashForce;
    public float DashCoolTime => _DashCoolTime;
}
