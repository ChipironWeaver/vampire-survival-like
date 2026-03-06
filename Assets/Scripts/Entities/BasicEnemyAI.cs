using System;
using UnityEngine;

public class BasicEnemyAI : MonoBehaviour
{
    [SerializeField] private Transform _target;
    [SerializeField] private float _moveSpeed = 1f;
    [SerializeField] private bool _rotationBasedMovement;

    private Transform _transform;
    private Rigidbody2D _rigidbody2D;
    private float _spriteRotation;
    private Vector2 _playerDirection;

    private void Start()
    {
        _transform = GetComponent<Transform>();
        _rigidbody2D = GetComponent<Rigidbody2D>();
        if (_target == null)
        {
            _target = PlayerController.Instance.transform;
        }
    }

    void FixedUpdate()
    {
        _playerDirection =  _target.position - _transform.position;
        _playerDirection.Normalize();
        if (_rotationBasedMovement) 
        {
            _spriteRotation = Vector2.Angle(Vector2.up, _playerDirection);
            if (_playerDirection.x > 0)
            {
                _spriteRotation = 180 - _spriteRotation + 180;
            }
            _transform.localRotation = Quaternion.Euler(0, 0 , _spriteRotation);
        }
        _rigidbody2D.linearVelocity = _moveSpeed * _playerDirection;
    }
}
