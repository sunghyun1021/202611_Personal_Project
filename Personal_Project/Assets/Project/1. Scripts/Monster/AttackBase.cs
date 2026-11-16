using System;
using UnityEngine;

public abstract class AttackBase : MonoBehaviour
{
    [SerializeField] protected MonsterData _monsterData;
    
    public Transform PlayerTransform;

    private void Awake()
    {
        _monsterData = GetComponent<MonsterData>();
    }

    public abstract void StartAttack();
}
