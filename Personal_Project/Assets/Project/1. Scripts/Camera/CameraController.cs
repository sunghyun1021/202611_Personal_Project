using System;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] private Transform _player;

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
        transform.position = new Vector3(_StartPosition.x, _StartPosition.y, transform.position.z);
        // transform.position = new Vector3(
        //     transform.position.x,
        //     transform.position.y,
        //     _player.position.z
        // );
    }
}
