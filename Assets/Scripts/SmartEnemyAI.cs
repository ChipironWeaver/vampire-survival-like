using System;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

public class SmartEnemyAI : MonoBehaviour
{
    [SerializeField] private Transform _target;
    [SerializeField] private bool _rotationSprite;
    [SerializeField] private Transform[] _childTransforms;

    private Transform _transform;
    private Rigidbody2D _rigidbody2D;
    private float _spriteRotation;
    private Vector2 _playerDirection;
    NavMeshAgent _agent;

    private void Start()
    {
        _transform = GetComponent<Transform>();
        _rigidbody2D = GetComponent<Rigidbody2D>();
        _agent = GetComponent<NavMeshAgent>();
        transform.rotation = Quaternion.Euler(Vector3.zero);
        _agent.updateRotation = false;
        if (_target == null)
        {
            _target = PlayerController.Instance.transform;
        }
    }

    void FixedUpdate()
    {
        
        _agent.SetDestination(_target.position);

        _playerDirection = _agent.desiredVelocity;
        _playerDirection.Normalize();
        
        if (_rotationSprite && _childTransforms.Length > 0) 
        {
            _spriteRotation = Vector2.Angle(Vector2.up, _playerDirection);
            if (_playerDirection.x > 0)
            {
                _spriteRotation = 180 - _spriteRotation + 180;
            }

            foreach (Transform childTransform in _childTransforms)
            {
                childTransform.rotation = Quaternion.Euler(0 , 0, _spriteRotation);
            }
        }
        
        
        
    }
}
