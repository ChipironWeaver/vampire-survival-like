using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UIElements;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float _speed = 1;
    [SerializeField] private Transform _spriteObject;

    private Vector2 _move;
    private Rigidbody2D _rb;
    private float _spriteRotation = 0f;

    void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
    }
    void Update()
    {
        if (_move != Vector2.zero)
        {
            _rb.linearVelocity = _speed * _move;
            _spriteRotation = Vector2.Angle(Vector2.up, _move);
            if (_move.x > 0)
            {
                _spriteRotation = 180-_spriteRotation + 180;
            }
        }
        else
        {
            _rb.linearVelocity = Vector3.zero;
            _spriteRotation = 0;
        }
        _spriteObject.localRotation = Quaternion.Euler(0, 0 , _spriteRotation);
    }
    void OnMove(InputValue value)
    {
        _move = value.Get<Vector2>();
    }
}
