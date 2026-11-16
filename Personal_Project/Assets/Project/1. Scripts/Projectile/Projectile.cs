using System;
using System.Collections;
using UnityEngine;

public class Projectile : MonoBehaviour, IPoolable
{
    [SerializeField] private LayerMask _enemyLayer;
    [SerializeField] private float _moveSpeed;
    [SerializeField] private float _destroyTime;

    private WaitForSeconds _waitForSeconds;

    public int Damage;
    
    public GameObject GameObject { get; }
    public ObjectPool Source { get; set; }

    // ---------- Event Method ----------

    private void Start() => Init();
    
    private void FixedUpdate()
    {
        transform.Translate(transform.up * (_moveSpeed * Time.deltaTime), Space.World);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (_enemyLayer.Contains(other))
        {
            other.GetComponent<IDamageable>()?.TakeDamage(Damage);
            Destroy(gameObject);
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
        Destroy(gameObject);
    }
    
    public void ReturnToPool()
    {
        Source.Push(this);
    }
}
