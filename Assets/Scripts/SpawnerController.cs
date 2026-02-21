using System;
using System.Collections.Generic;
using UnityEngine;
using Random = System.Random;

public class SpawnerController : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private List<GameObject> _enemiesPrefabs = new List<GameObject>();
    [SerializeField] private List<Transform> _spawnerTransform = new List<Transform>();
    [Header("Timing Settings")]
    [SerializeField] private float _baseCooldown = 5f;
    [SerializeField] private float _cooldownDecrease = 1f;
    [SerializeField] private float _baseEnemyAmount = 1f;
    [SerializeField] private float _enemyAmountMultiplier = 1f;
    [SerializeField] private List<string> _seed;
    
    private float _currentCooldown;
    private float _enemyAmount;
    private List<GameObject> _enemies = new List<GameObject>();
    private Random _random =  new Random();
    private GameObject _enemiesParent;

    private void OnEnable()
    {
        LevelController.onGameOver += DestroyEnemies;
        LevelController.onStartGame += GameStart;
    }

    private void OnDisable()
    {
        LevelController.onGameOver -= DestroyEnemies;
        LevelController.onStartGame -= GameStart;
    }


    private void GameStart()
    {
        _random = new Random(_seed[_random.Next(0,_seed.Count)].GetHashCode());
        if (_enemiesParent == null)
        {
            _enemiesParent = new GameObject();
            _enemiesParent.transform.SetParent(transform);
            _enemiesParent.name = "Enemies";
        }
        _currentCooldown = _baseCooldown;
        _enemyAmount = _baseEnemyAmount;
    }
    private void Update()
    {
        if (LevelController.gameIsRunning)
        {
            TrySpawnEnemies();
        }
    }

    private void TrySpawnEnemies()
    {
        if (_currentCooldown >= _baseCooldown)
        {
            for (int i = 1 ; i <= _enemyAmount; i++)
            {
                Vector2 _spawnPosition = _spawnerTransform[_random.Next(0, _spawnerTransform.Count)].position;
                GameObject _enemieSelected =  _enemiesPrefabs[_random.Next(0, _enemiesPrefabs.Count)];
                _enemies.Add(InstantiateEnemy(_spawnPosition, _enemieSelected));
            }

            _enemyAmount *= _enemyAmountMultiplier;
            _baseCooldown -= _cooldownDecrease;
            
            
            if (_baseCooldown < 0.5f) _baseCooldown = 0.5f ;
            _currentCooldown = 0f;
            
        }
        else
        {
            _currentCooldown += Time.deltaTime;
        }
    }

    private GameObject InstantiateEnemy(Vector2 position, GameObject prefab)
    {
        GameObject enemy = Instantiate(prefab, position, Quaternion.identity);
        enemy.transform.SetParent(_enemiesParent.transform);
        return enemy;
    }

    public void DestroyEnemies(bool win)
    {
        foreach(GameObject gameObject in  _enemies) 
        {
            //different particle played if win or lose
            Destroy(gameObject);
        }
        _enemies.Clear();
    }
}
