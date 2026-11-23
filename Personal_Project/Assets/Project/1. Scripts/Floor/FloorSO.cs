using UnityEngine;

[CreateAssetMenu(fileName = "FloorSO", menuName = "ScriptableObjects/Floor/FloorSO", order = 1)]
public class FloorSO : ScriptableObject
{
    public int spawnMonsterAmount;
    public MonsterType[] spawnMonsterType;

}
