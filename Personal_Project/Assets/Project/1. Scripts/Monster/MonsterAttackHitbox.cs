using System;
using System.Collections.Generic;
using UnityEngine;

public class MonsterAttackHitbox : MonoBehaviour
{
    [SerializeField] private Collider hitboxCollider;
    [SerializeField] private LayerMask _targetLayerMask;
    
    private MonsterData _monsterData;

    private HashSet<Collider> _hitTargets = new();
    
    // ---------- Event Method ----------

    private void Awake() => CacheComponents();
    
    private void OnTriggerEnter(Collider other)
    {
        if (!_targetLayerMask.Contains(other)) return;

        if (!_hitTargets.Add(other)) return;
        
        IDamageable damageable = other.gameObject.GetComponent<IDamageable>();
        
        if (damageable != null)
        {
            damageable.TakeDamage(_monsterData.AttackDamage);
        }

    }

    private void OnDisable()
    {
        hitboxCollider.enabled = false;
    }

    // ----------------------------------
    
    private void CacheComponents()
    {
        _monsterData = GetComponentInParent<MonsterData>();
    }

    public void OnEnableHitbox()
    {
        Debug.Log("Hitbox Enabled");
        _hitTargets.Clear();
        hitboxCollider.enabled = true;
    }

    public void OnDisableHitbox()
    {
        Debug.Log("Hitbox Disabled");
        hitboxCollider.enabled = false;
    }
    
}
