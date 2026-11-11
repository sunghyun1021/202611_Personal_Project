using System;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] private Transform _player;
    [SerializeField] private float _clampValue;

    private Vector3 _StartPosition = new(0f, 4f, -30f);
    private Vector3 _StartRotation = new(10f, 0f, 0f);
        
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
        
        float positionX = Mathf.Clamp(transform.position.x, -_clampValue, _clampValue);
        
        transform.position = new Vector3(positionX, _StartPosition.y, _StartPosition.z);
    }
}
