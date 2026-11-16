using System;
using UnityEngine;

public class MonsterData : MonoBehaviour
{
    [SerializeField] private MonsterSO _monsterSO;
    
    public ObservableProperty<int> CurrentHealth;
    public int MaxHealth;
    public int AttackDamage;
    public float MoveSpeed;

    public Action<MonsterController> OnDead;

    private void Awake()
    {
        InitData();
    }

    public void InitData()
    {
        if (_monsterSO == null)
        {
            Debug.Log($"MonsterData._monsterSO : null");
            return;
        }
        
        CurrentHealth.Value = _monsterSO.maxHealth;
        MaxHealth = _monsterSO.maxHealth;
        AttackDamage = _monsterSO.attackDamage;
        MoveSpeed = _monsterSO.moveSpeed;
    }
}
