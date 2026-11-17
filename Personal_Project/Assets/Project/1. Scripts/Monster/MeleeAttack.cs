using System.Collections;
using UnityEngine;

public class MeleeAttack : AttackBase
{
    private float _elapsedTime;
    private float _animationTime;

    
        
    public override void StartAttack()
    {
        _animator.SetTrigger("Attack");
        IsAttacking = true;
        StartCoroutine(WaitAttackEnd());
    }

    private IEnumerator WaitAttackEnd()
    {
        transform.position = new Vector3(transform.position.x, 0f, 0f);
        
        yield return null;
        
        _animationTime = _animator.GetCurrentAnimatorStateInfo(0).length;
        _elapsedTime = 0;
     
        while (_elapsedTime <= _animationTime)
        {
            _elapsedTime += Time.deltaTime;
            yield return null;
        }

        IsAttacking = false;
    }
}
