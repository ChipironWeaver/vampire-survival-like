using System;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class Score : MonoBehaviour
{
    [SerializeField] private float _baseScore;
    [SerializeField] private float _baseScoreMultiplier;
    static float score = 100;
    static float scoreMultiplier = 3;
    static TextMeshProUGUI _scoreText;

    private void Start()
    {
        _scoreText = GetComponent<TextMeshProUGUI>();
        GameStart();
    }

    private void GameStart()
    {
        score = _baseScore;
        scoreMultiplier = _baseScoreMultiplier;
        UpdateScore();
    }
    
    
    static void UpdateScore(float tempScore = 0, float tempScoreMultiplier = 0)
    {
        score += tempScore;
        scoreMultiplier += tempScoreMultiplier;
        int intScore =  (int)(score * scoreMultiplier); 
        _scoreText.SetText(intScore.ToString(),true);
    }

    private void OnEnable()
    {
        HealthController.onPlayerDamage += DamageUpdate;
        HealthController.onPlayerHeal += HealUpdate;
    }

    private void OnDisable()
    {
        HealthController.onPlayerDamage -= DamageUpdate;
        HealthController.onPlayerHeal -= HealUpdate;
    }

    private void DamageUpdate()
    {
        UpdateScore(-100,-1);
    }

    private void HealUpdate()
    {
        UpdateScore(75,0.5f);
    }
    
    private void PickupUpdate()
    {
        UpdateScore(50);
    }
}
