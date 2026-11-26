using System;
using System.Collections;
using UnityEngine;

public class Projectile : MonoBehaviour, IPoolable
{
    [SerializeField] private PoolableType _poolType;
    [SerializeField] private LayerMask _enemyLayer;
    [SerializeField] private float _destroyTime;

    private WaitForSeconds _waitForSeconds;

    public PoolableType PoolType => _poolType;
    public float MoveSpeed;
    public int Damage;
    
    public GameObject GameObject { get => gameObject; }
    public ObjectPool Source { get; set; }

    // ---------- Event Method ----------

    private void Start() => Init();
    
    private void FixedUpdate()
    {
        transform.Translate(transform.up * (MoveSpeed * Time.deltaTime), Space.World);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (_enemyLayer.Contains(other))
        {
            other.GetComponent<IDamageable>()?.TakeDamage(Damage);
            Debug.Log($"Damage : {Damage}, MoveSpeed : {MoveSpeed}");
            ReturnToPool();
        }
    }

    // ----------------------------------
    
    private void Init()
    {
        _waitForSeconds =  new WaitForSeconds(_destroyTime);
        StartCoroutine(DestroyObject());
    }

    private IEnumerator DestroyObject()
    {
        yield return _waitForSeconds;
        ReturnToPool();
    }
    
    public void ReturnToPool()
    {
        Source.Push(this);
    }

    public void SetData(float speed, int damage)
    {
        MoveSpeed = speed;
        Damage = damage;
    }
}
