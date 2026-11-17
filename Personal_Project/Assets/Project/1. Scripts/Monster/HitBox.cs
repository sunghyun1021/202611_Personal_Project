using System;
using System.Collections.Generic;
using UnityEngine;

public class HitBox : MonoBehaviour
{
    [SerializeField] private MonsterData _monsterData;
    [SerializeField] private LayerMask _targetLayerMask;
    
    public HashSet<Collider> HitTargets = new();
    public Collider HitCollider;

    // ---------- Event Method ----------

    private void Awake() => CacheComponents();

    private void OnTriggerEnter(Collider other)
    {
        if (!_targetLayerMask.Contains(other))
        {
            return;
        }
        
        if (!HitTargets.Add(other))
        {
            return;
        }
        
        IDamageable damageable = other.gameObject.GetComponent<IDamageable>();
        
        if (damageable != null)
        {
            damageable.TakeDamage(_monsterData.AttackDamage);
        }
    }
    
    // ----------------------------------

    private void CacheComponents()
    {
        HitCollider = GetComponent<Collider>();
    }
}
