using System;
using System.Collections;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField] private float _moveSpeed;
    [SerializeField] private float _destroyTime;

    private WaitForSeconds _waitForSeconds;

    // ---------- Event Method ----------

    private void Start() => Init();
    
    private void FixedUpdate()
    {
        transform.Translate(transform.up * (_moveSpeed * Time.deltaTime), Space.World);
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
}
