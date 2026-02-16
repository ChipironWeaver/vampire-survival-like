using System;
using UnityEngine;

public class BasicEnemyAI : MonoBehaviour
{
    [SerializeField] private Transform _target = null;
    [SerializeField] private float _moveSpeed = 1f;
    
    private Transform _transform;

    private void Start()
    {
        _transform = GetComponent<Transform>();
    }

    void FixedUpdate()
    {
        if (_target != null)
        {
            _transform.position = Vector3.MoveTowards(_transform.position, _target.position, _moveSpeed * Time.deltaTime);
        }
    }
}
