using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class UIFlash : MonoBehaviour
{
    
    [Header("Health Flash")]
    [SerializeField] private Color _healthFlashColor;
    [SerializeField] private float _healthFlashDuration;
    [Header("Damage Flash")]
    [SerializeField] private Color _damageFlashColor;
    [SerializeField] private float _damageFlashDuration;
    [Header("Win Flash")]
    [SerializeField] private Color _winFlashColor;
    [SerializeField] private float _winFlashDuration;
    [Header("Lose Flash")]
    [SerializeField] private Color _loseFlashColor;
    [SerializeField] private float _loseFlashDuration;
    [Header("Score Flash")]
    [SerializeField] private Color _scoreFlashColor;
    [SerializeField] private float _scoreFlashDuration;
    [Header("Speed Flash")]
    [SerializeField] private Color _speedFlashColor;
    [SerializeField] private float _speedFlashDuration;
    
    
    public static Image flash;
    static UIFlash _instance;
    public static UIFlash Instance
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
    private void OnEnable()
    {
        HealthController.onPlayerDamage += DamageFlash;
        LevelController.onGameOver += EndFlash;
        flash = GetComponent<Image>();
    }

    private void OnDisable()
    {
        HealthController.onPlayerDamage -= DamageFlash;
        LevelController.onGameOver -= EndFlash;
    }

    private void DamageFlash()
    {
        StartCoroutine(CoroutineFlash(_damageFlashColor, _damageFlashDuration));
    }
    private void EndFlash(bool win)
    {
        if (win)
        {
            StartCoroutine(CoroutineFlash(_winFlashColor, _winFlashDuration));
        }
        else
        {
            StartCoroutine(CoroutineFlash( _loseFlashColor, _loseFlashDuration));
        }
    }

    public static IEnumerator CoroutineFlash(Color color, float duration)
    {
        float currentTime = 0;
        while (currentTime < duration)
        {
            flash.color = Color.Lerp(Color.clear, color, Mathf.PingPong(currentTime * 2 / duration, 1));
            currentTime += Time.deltaTime;
            yield return new WaitForNextFrameUnit();
        }
    }
}
