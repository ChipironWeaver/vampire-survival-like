using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [SerializeField] public float speed = 1;
    private Vector2 _move;
    private Rigidbody2D _rb;
    private float _spriteRotation;
    private Transform _transform;
    private Animator _animator;
    static PlayerController _instance;
    
    public static PlayerController Instance
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
    
    void OnEnable()
    {
        _rb = GetComponent<Rigidbody2D>();
        _transform = GetComponent<Transform>();
        _animator = GetComponent<Animator>();
        LevelController.onStartGame += GameStart;
    }

    void OnDisable()
    {
        LevelController.onStartGame -= GameStart;
    }

    void GameStart()
    {
        transform.position = Vector3.zero;
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
            _rb.linearVelocity = speed * _move;
            _animator.SetBool("IsMoving", true);
        }
        else
        {
            _rb.linearVelocity = Vector3.zero;
            _animator.SetBool("IsMoving", false);
        }
        _transform.localRotation = Quaternion.Euler(0, 0 , _spriteRotation);
    }

    void OnMove(InputValue value)
    {
        if (LevelController.gameIsRunning)
        {
            _move = value.Get<Vector2>();
        }
        else
        {
            _move = Vector2.zero;
        }
    }
}
