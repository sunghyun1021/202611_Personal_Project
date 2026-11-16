using System;
using UnityEngine;

public abstract class AttackBase : MonoBehaviour
{
    [SerializeField] protected Transform _playerTransform;
    [SerializeField] protected MonsterData _monsterData;

    private void Awake()
    {
        _monsterData = GetComponent<MonsterData>();
    }

    public abstract void StartAttack();
}
