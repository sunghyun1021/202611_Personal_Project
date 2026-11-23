using UnityEngine;

public class FlowManager : SingletonBehaviour<FlowManager>
{
    [SerializeField] private spawnPoint[] spawnPoints;
    
    // ---------- Event Method ----------

    private void Awake() => SetSingleton();
    
    // ---------- private Method ----------
    
    // ---------- public Method ----------

    public void SpawnMonster()
    {
        IPoolable pool;
        
        pool = PoolManager.Instance.Take(MonsterType.Melee);
        
        int temp = Random.Range(0, spawnPoints.Length);
        pool.GameObject.transform.position = spawnPoints[temp].transform.position;
        pool.GameObject.transform.LookAt(Vector3.zero);
    }
}
