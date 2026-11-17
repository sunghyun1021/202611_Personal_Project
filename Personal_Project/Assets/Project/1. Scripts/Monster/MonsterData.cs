using System;
using Unity.Behavior;
using UnityEngine;

public class MonsterData : MonoBehaviour
{
    [SerializeField] private MonsterSO _monsterSO;

    private BehaviorGraphAgent _behaviorGraphAgent;
    
    public ObservableProperty<int> CurrentHealth;
    public int MaxHealth;
    public int AttackDamage;
    public float MoveSpeed;
    public float AttackCoolTime;
    public float AttackRange;
    public Action<MonsterController> OnDead;

    // ---------- Event Method ----------
    
    private void Awake() => CacheComponents();


    private void Start() => InitData();
    
    // ----------------------------------

    private void CacheComponents()
    {
        _behaviorGraphAgent = GetComponent<BehaviorGraphAgent>();
    }

    public void InitData()
    {
        if (_monsterSO == null)
        {
            Debug.Log($"MonsterData._monsterSO : null");
            return;
        }

        SetData();
        if (_behaviorGraphAgent == null)
        {
            _behaviorGraphAgent = GetComponent<BehaviorGraphAgent>();
        }
        SetBehaviorData();
    }

    private void SetData()
    {
        CurrentHealth.Value = _monsterSO.maxHealth;
        MaxHealth = _monsterSO.maxHealth;
        AttackDamage = _monsterSO.attackDamage;
        MoveSpeed = _monsterSO.moveSpeed;
        AttackCoolTime = _monsterSO.attackCoolTime;
        AttackRange = _monsterSO.attackRange;
    }

    private void SetBehaviorData()
    {
        _behaviorGraphAgent.SetVariableValue("moveSpeed", _monsterSO.moveSpeed);
        _behaviorGraphAgent.SetVariableValue("attackCoolTime", _monsterSO.attackCoolTime);
        _behaviorGraphAgent.SetVariableValue("attackRange", _monsterSO.attackRange);
    }
}
