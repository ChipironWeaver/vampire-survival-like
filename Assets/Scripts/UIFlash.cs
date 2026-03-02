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
    
    private Image _flash;
    private void OnEnable()
    {
        HealthController.onPlayerDamage += DamageFlash;
        HealthController.onPlayerHeal += HealFlash;
        _flash = GetComponent<Image>();
    }

    private void OnDisable()
    {
        HealthController.onPlayerDamage -= DamageFlash;
        HealthController.onPlayerHeal -= HealFlash;
    }

    private void DamageFlash()
    {
        StartCoroutine(CoroutineFlash(_damageFlashColor, _damageFlashDuration));
    }

    private void HealFlash()
    {
        StartCoroutine(CoroutineFlash(_healthFlashColor, _healthFlashDuration));
    }

    private IEnumerator CoroutineFlash(Color color, float duration)
    {
        float currentTime = 0;
        while (currentTime < duration)
        {
            _flash.color = Color.Lerp(Color.clear, color, Mathf.PingPong(currentTime * 2 / duration, 1));
            currentTime += Time.deltaTime;
            yield return new WaitForNextFrameUnit();
        }
    }
}
