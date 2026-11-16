using System;
using Unity.Behavior;
using Unity.VisualScripting;
using UnityEngine;

public class MonsterController : MonoBehaviour, IPoolable, IDamageable
{
    [field: Header("몬스터 타입")]
    [field: SerializeField] public MonsterType Type { get; set; }

    [Header("몬스터 공격 타입")] 
    [SerializeField] private AttackBase _attackType;
    
    [SerializeField] private Transform _playerTransform;

    private MonsterData _monsterData;
    private BehaviorGraphAgent _behaviorAgent;
    private Rigidbody _rigidbody;
    
    public GameObject GameObject { get => gameObject; }
    public ObjectPool Source { get; set; }
    
    
    // ---------- Event Method ----------

    private void Awake() => CacheComponents();

    private void OnEnable() => DataReset();

    private void Start() => Init();

    // ----------------------------------

    private void CacheComponents()
    {
        _monsterData = GetComponent<MonsterData>();
        _behaviorAgent = GetComponent<BehaviorGraphAgent>();
        _rigidbody = GetComponent<Rigidbody>();
    }

    private void Init()
    {
        if (_attackType == null) _attackType = GetComponent<AttackBase>();
        _behaviorAgent.SetVariableValue("Player", _playerTransform);
        _behaviorAgent.SetVariableValue("attackType", _attackType);
    }

    private void DataReset()
    {
        _monsterData.InitData();
    }
    
    public void TakeDamage(int damage)
    {
        _monsterData.CurrentHealth.Value -= damage;

        if (_monsterData.CurrentHealth.Value <= 0)
        {
            Dead();
        }
    }

    public void Dead()
    {
        _monsterData.OnDead.Invoke(this);
     
        // TODO 애니메이션 실행, 잔여 몬스터 카운트 감소
        
        ReturnToPool();
    }
    
    public void ReturnToPool()
    {
        Source.Push(this);
    }
}
