using UnityEngine;

public class MeleeAttack : AttackBase
{
    public override void StartAttack()
    {
        Debug.Log("공격중");
        transform.position = new Vector3(transform.position.x, 0f, 0f);
    }
}
