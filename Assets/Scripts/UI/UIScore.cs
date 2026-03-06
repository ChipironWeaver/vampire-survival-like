using System.Collections;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class UIScore : MonoBehaviour
{
    [SerializeField] private float _baseScore;
    [SerializeField] private float _baseScoreMultiplier;
    [SerializeField] private float _difficultyMultiplier;
    [Header("Fade out color")]
    [SerializeField] private Color _winColor;
    [SerializeField] private Color _loseColor;
    [SerializeField] private float _fadeOutDuration;
    [SerializeField] private Color _baseColor;
    [Header("Damage Score")]
    [SerializeField] private float _damageScore;
    [SerializeField] private float _damageScoreMultiplier;
    [Header("Health Score")]
    [SerializeField] private float _healthScore;
    [SerializeField] private float _healthScoreMultiplier;
    public static Color textColor = Color.white;
    
    public static float score = 100;
    public static float scoreMultiplier = 3;
    public static float difficultyMultiplier = 1;
    static TextMeshProUGUI _scoreText;

    static UIScore _instance;
    public static UIScore instance
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

    private void GameStart()
    {
        score = _baseScore;
        scoreMultiplier = _baseScoreMultiplier;
        difficultyMultiplier = _difficultyMultiplier;
        UpdateScore();
        _scoreText.color = _baseColor;
    }
    
    public static void UpdateScore(float tempScore = 0, float tempScoreMultiplier = 0)
    {
        score += tempScore;
        scoreMultiplier += tempScoreMultiplier;
        int intScore = (int)(score * scoreMultiplier * difficultyMultiplier); 
        _scoreText.SetText(intScore.ToString());
        _scoreText.color = textColor;
    }

    private void OnEnable()
    {
        HealthController.onPlayerDamage += DamageUpdate;
        HealthController.onPlayerHeal += HealUpdate;
        LevelController.onStartGame += GameStart;
        LevelController.onGameOver += StartFadeOut;
        _scoreText = GetComponent<TextMeshProUGUI>();
    }

    private void OnDisable()
    {
        HealthController.onPlayerDamage -= DamageUpdate;
        HealthController.onPlayerHeal -= HealUpdate;
        LevelController.onStartGame -= GameStart;
        LevelController.onGameOver -= StartFadeOut;
    }

    private void DamageUpdate()
    {
        UpdateScore(_damageScore,_damageScoreMultiplier);
    }

    private void HealUpdate()
    {
        UpdateScore(_healthScore,_healthScoreMultiplier);
    }
    
    IEnumerator FadeOut(bool win)
    {
        float currentTime = 0f;
        while (currentTime < _fadeOutDuration)
        {
            currentTime += Time.deltaTime;
            _scoreText.color = Color.Lerp(Color.white, win ? _winColor : _loseColor, currentTime / _fadeOutDuration);
            yield return new WaitForNextFrameUnit();
        }
    }

    private void StartFadeOut(bool win)
    {
        StartCoroutine(FadeOut(win));
    }
}
