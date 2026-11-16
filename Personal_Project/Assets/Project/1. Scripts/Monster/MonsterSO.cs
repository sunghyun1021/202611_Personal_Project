using UnityEngine;

[CreateAssetMenu(fileName = "MonsterSO", menuName = "ScriptableObjects/MonsterSO", order = 1)]
public class MonsterSO : ScriptableObject
{
    public int maxHealth;
    public int attackDamage;
    public float moveSpeed;
    public float attackCoolTime;
    public float attackRange;
}
