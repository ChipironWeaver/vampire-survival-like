using System;
using System.Collections;
using UnityEngine;

public class LevelController : MonoBehaviour
{
    [SerializeField] private float _maxGameTime = 60f;
    [SerializeField] private int _overTimeScore;    
    public delegate void OnStartGame();
    public static event OnStartGame onStartGame;

    public delegate void OnGameOver(bool win);
    public static event OnGameOver onGameOver;

    public static float CurrentGameTime;
    public static float MaxGameTime = 60f;
    public static bool gameIsRunning = false;
    
    private float _currentScoreCooldown = 0f;
    
    static LevelController _instance;
    public static LevelController instance
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

    void Start()
    {
        GameStart();
    }

    public void GameStart()
    {
        onStartGame?.Invoke();
        gameIsRunning = true;
        MaxGameTime = _maxGameTime;
    }

    public static void GameOver(bool win)
    {
        onGameOver?.Invoke(win);
        gameIsRunning = false;
    }

    public void Update()
    {
        if (gameIsRunning)
        {
            CurrentGameTime += Time.deltaTime;
            _currentScoreCooldown += Time.deltaTime;
            if (_currentScoreCooldown >= 1f)
            {
                Score.UpdateScore(_overTimeScore);
                _currentScoreCooldown -= 1f;
            }
            if (CurrentGameTime >= _maxGameTime) GameOver(true);
        }
    }
}
