using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovePlatform : MonoBehaviour
{
    [SerializeField] private float _distance;
    [SerializeField] private Vector3 _speed;
    
    private Vector3 _startPoint;
    
    private float _direction = 1;
    
    private void Start()
    {
        _startPoint = transform.localPosition;
    }

    private void Update()
    {
        transform.position += _speed * _direction * Time.deltaTime;

        float distance = Vector3.Distance(transform.localPosition, _startPoint);

        if(distance > _distance * 2)
            transform.localPosition = _startPoint;
        
        if(distance > _distance)
            _direction *= -1;
    }
}
