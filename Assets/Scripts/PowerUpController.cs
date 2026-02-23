using System.Collections;
using UnityEngine;

public class PowerUpController : MonoBehaviour
{
    [SerializeField] private float _buffDuration;
    [SerializeField] private int _healAmount;
    [SerializeField] private float _speedBoost;
    [SerializeField] private float _scoreBoost;
    [SerializeField] private float _scoreMultiplierBoost;
    
    private SpriteRenderer _spriteRenderer;
    private Collider2D _collider;

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
            GameObject player =  collision.gameObject;
            if (_healAmount > 0)
            {
                player.GetComponent<HealthController>().Heal(_healAmount);
            }

            if (_speedBoost > 0)
            {
                player.GetComponent<PlayerController>().speed += _speedBoost;
            }

            if (_scoreBoost > 0 || _scoreMultiplierBoost > 0)
            {
                Score.UpdateScore(_scoreBoost, _scoreMultiplierBoost);
            }

            StartCoroutine(DestroySelf(player));
        }
    }
    
    private IEnumerator DestroySelf(GameObject player)
    {
        yield return new WaitForSeconds(_buffDuration);
        if (_speedBoost > 0)
        {
            player.GetComponent<PlayerController>().speed -= _speedBoost;
        }
        Destroy(this.gameObject);
    }
}
