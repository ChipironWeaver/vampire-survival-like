using System.Collections;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class Score : MonoBehaviour
{
    [SerializeField] private float _baseScore;
    [SerializeField] private float _baseScoreMultiplier;
    [Header("Fade out color")]
    [SerializeField] private Color _winColor;
    [SerializeField] private Color _loseColor;
    [SerializeField] private float _fadeOutDuration;
    static float score = 100;
    static float scoreMultiplier = 3;
    static TextMeshProUGUI _scoreText;

    static Score _instance;
    public static Score instance
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
        UpdateScore();
        _scoreText.color = Color.white;
    }
    
    public static void UpdateScore(float tempScore = 0, float tempScoreMultiplier = 0)
    {
        score += tempScore;
        scoreMultiplier += tempScoreMultiplier;
        int intScore = (int)(score * scoreMultiplier); 
        _scoreText.SetText(intScore.ToString());
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
        UpdateScore(-50,-0.75f);
    }

    private void HealUpdate()
    {
        UpdateScore(30,0.5f);
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
