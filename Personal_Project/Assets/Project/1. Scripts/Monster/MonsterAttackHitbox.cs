using UnityEngine;

public class MonsterAttackHitbox : MonoBehaviour
{
    [SerializeField] private HitBox _hitbox;
    
    // ---------- Event Method ----------
    
    private void OnDisable()
    {
        _hitbox.HitCollider.enabled = false;
    }

    // ----------------------------------
    
    public void OnEnableHitbox()
    {
        _hitbox.HitCollider.enabled = true;
    }

    public void OnDisableHitbox()
    {
        _hitbox.HitTargets.Clear();
        _hitbox.HitCollider.enabled = false;
    }
    
}
