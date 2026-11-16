using System;
using UnityEngine;

public abstract class AttackBase : MonoBehaviour
{
    [SerializeField] protected MonsterData _monsterData;
    [SerializeField] protected Animator _animator;
    
    public Transform PlayerTransform;

    private void Awake()
    {
        _monsterData = GetComponent<MonsterData>();
        _animator = GetComponentInChildren<Animator>();
    }

    public abstract void StartAttack();
}
