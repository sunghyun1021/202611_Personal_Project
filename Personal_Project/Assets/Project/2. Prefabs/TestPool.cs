
using UnityEngine;

public class TestPool : MonoBehaviour, IPoolable
{
    [field:SerializeField] public PoolableType PoolType { get; private set; }
    public GameObject GameObject { get => gameObject; }
    public ObjectPool Source { get; set; }
    
    
    public void ReturnToPool()
    {
        
    }
}
