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
            Debug.Log("LayerMask 아님");
            return;
        }
        
        if (!HitTargets.Add(other))
        {
            Debug.Log("hitTarget 있음");
            return;
        }
        
        IDamageable damageable = other.gameObject.GetComponent<IDamageable>();
        
        if (damageable != null)
        {
            damageable.TakeDamage(100);
        }
    }
    
    // ----------------------------------

    private void CacheComponents()
    {
        HitCollider = GetComponent<Collider>();
    }
}
