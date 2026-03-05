using System.Collections;
using UnityEngine;

public class PowerUpController : MonoBehaviour
{
    [SerializeField] private float _buffDuration;
    [SerializeField] private int _healAmount;
    [SerializeField] private float _speedBoost;
    [SerializeField] private float _scoreBoost;
    [SerializeField] private float _scoreMultiplierBoost;
    [SerializeField] private AudioClip _clip;
    [SerializeField] private Color _flashColor = Color.clear;
    [SerializeField] private float _flashDuration;
    
    private SpriteRenderer _spriteRenderer;
    private Collider2D _collider;
    private GameObject _player;

    
    public delegate void PickUpFlash(Color color, float duration);
    public static event PickUpFlash onPickUpFlash;
    
    void OnEnable()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _collider = GetComponent<Collider2D>();
    }
    
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            _collider.enabled = false;
            _spriteRenderer.enabled = false;
            _player =  collision.gameObject;
            if (_healAmount > 0)
            {
                _player.GetComponent<HealthController>().Heal(_healAmount);
            }

            if (_speedBoost > 0)
            {
                _player.GetComponent<PlayerController>().speed += _speedBoost;
            }

            if (_scoreBoost > 0 || _scoreMultiplierBoost > 0)
            {
                Score.UpdateScore(_scoreBoost, _scoreMultiplierBoost);
            }

            onPickUpFlash?.Invoke(_flashColor, _flashDuration);
            
            if (_clip != null) AudioSource.PlayClipAtPoint(_clip, Vector3.zero);
            if(_buffDuration >= 0) StartCoroutine(DestroySelf());
        }
    }
    
    private IEnumerator DestroySelf()
    {
        yield return new WaitForSeconds(_buffDuration);
        if (_speedBoost > 0) _player.GetComponent<PlayerController>().speed -= _speedBoost;
        Destroy(this.gameObject);
    }
}
