using UnityEngine;

public class FlowManager : SingletonBehaviour<FlowManager>
{
    [SerializeField] private spawnPoint[] spawnPoints;
    
    // ---------- Event Method ----------

    private void Awake() => SetSingleton();
    
    // ----------------------------------

    public void SpawnMonster()
    {
        IPoolable pool;
        
        pool = PoolManager.Instance.Take(MonsterType.Melee);
        
        int temp = Random.Range(0, spawnPoints.Length);
        pool.GameObject.transform.position = spawnPoints[temp].transform.position;
        pool.GameObject.transform.LookAt(Vector3.zero);
    }
}
