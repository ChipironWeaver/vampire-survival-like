using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UIElements;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float _speed = 1;
    private Vector2 _move;
    private Rigidbody2D _rb;
    private float _spriteRotation = 0f;
    private Transform _transform;

    static PlayerController _instance;
    public static PlayerController instance
    {
        get
        {
            return _instance;
        }
    }
  
    void Awake()
    {
        _instance = this;
    }
    
    void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
        _transform = GetComponent<Transform>();
    }
    void Update()
    {
        if (_move != Vector2.zero)
        {
            
            _spriteRotation = Vector2.Angle(Vector2.up, _move);
            if (_move.x > 0)
            {
                _spriteRotation = 180-_spriteRotation + 180;
            }
            _rb.linearVelocity = _speed * _move;
        }
        else
        {
            _rb.linearVelocity = Vector3.zero;
        }
        _transform.localRotation = Quaternion.Euler(0, 0 , _spriteRotation);
    }
    void OnMove(InputValue value)
    {
        _move = value.Get<Vector2>();
    }
    
}
