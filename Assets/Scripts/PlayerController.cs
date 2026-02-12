using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float _speed = 1;

    private Vector3 _move;
    
    void Update()
    {
        transform.Translate( _speed * Time.deltaTime * _move);
    }
    void OnMove(InputValue value)
    {
        _move = value.Get<Vector2>();
    }
}
