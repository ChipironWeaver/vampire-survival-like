using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float _baseSpeed = 1;
    public float speed;
    private Animator _animator;
    private Vector2 _move;
    private Rigidbody2D _rb;
    private float _spriteRotation;
    private Transform _transform;

    public static PlayerController Instance { get; private set; }

    private void Awake()
    {
        Instance = this;
    }

    private void Update()
    {
        if (_move != Vector2.zero)
        {
            _spriteRotation = Vector2.Angle(Vector2.up, _move);
            if (_move.x > 0) _spriteRotation = 180 - _spriteRotation + 180;
            _rb.linearVelocity = speed * _move;
            _animator.SetBool("IsMoving", true);
        }
        else
        {
            _rb.linearVelocity = Vector3.zero;
            _animator.SetBool("IsMoving", false);
        }

        _transform.localRotation = Quaternion.Euler(0, 0, _spriteRotation);
    }

    private void OnEnable()
    {
        _rb = GetComponent<Rigidbody2D>();
        _transform = GetComponent<Transform>();
        _animator = GetComponent<Animator>();
        LevelController.onStartGame += GameStart;
    }

    private void OnDisable()
    {
        LevelController.onStartGame -= GameStart;
    }

    private void GameStart()
    {
        transform.position = Vector3.zero;
        speed = _baseSpeed;
    }

    private void OnMove(InputValue value)
    {
        if (LevelController.GameIsRunning)
            _move = value.Get<Vector2>();
        else
            _move = Vector2.zero;
    }
}