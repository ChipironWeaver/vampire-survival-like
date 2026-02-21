using System;
using UnityEngine;

public class LevelController : MonoBehaviour
{
    public delegate void OnStartGame();
    public static event OnStartGame onStartGame;

    public delegate void OnGameOver(bool win);
    public static event OnGameOver onGameOver;
    
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

    public float gameTime = 60f;
    public float currentGameTime = 0f;
    public static bool gameIsRunning = false;

    public static void GameStart()
    {
        onStartGame?.Invoke();
        gameIsRunning = true;
    }

    public static void GameOver(bool win = true)
    {
        onGameOver?.Invoke(win);
        gameIsRunning = false;
    }
}
