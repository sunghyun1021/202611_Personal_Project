using System;
using UnityEngine;

public class Slot :MonoBehaviour
{
    private RectTransform _rectTransform;
    
    public bool IsItemIn;
    public Vector2Int CenterPosition;

    private void Awake()
    {
        _rectTransform = GetComponent<RectTransform>();
    }

    private void Start()
    {
        _rectTransform.TransformPoint(_rectTransform.rect.center);
        CenterPosition.x = (int)_rectTransform.rect.center.x;
        CenterPosition.y = (int)_rectTransform.rect.center.y;
    }

    private void Update()
    {
        //Debug.Log(_rectTransform.TransformPoint(_rectTransform.rect.center));
    }
}   
