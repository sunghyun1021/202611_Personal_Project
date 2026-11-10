using System;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] private Transform _player;
    [SerializeField] private float _clampValue;

    private Vector3 _StartPosition = new(20f, 4f, 0f);
    private Vector3 _StartRotation = new(10f, -90f, 0f);
        
    // ---------- Event Method ----------
    
    private void Awake()
    {
        transform.position = _StartPosition;
        transform.rotation = Quaternion.Euler(_StartRotation);
    }

    private void FixedUpdate()
    {
        Move();
    }

    // ----------------------------------

    private void Move()
    {
        transform.position = Vector3.Lerp(transform.position, _player.position, Time.deltaTime * 2);
        
        float positionZ = Mathf.Clamp(transform.position.z, -_clampValue, _clampValue);
        
        transform.position = new Vector3(_StartPosition.x, _StartPosition.y, positionZ);
    }
}
