using UnityEngine;

public class MeleeAttack : AttackBase
{
    public override void StartAttack()
    {
        _animator.SetTrigger("Attack");
        transform.position = new Vector3(transform.position.x, 0f, 0f);
    }
}
