using System;
using System.Collections.Generic;
using System.Data;
using UnityEngine;
using Random = System.Random;

public class SpawnerController : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private List<EnemyWeight> _enemiesPrefab = new List<EnemyWeight>();
    [SerializeField] private List<Transform> _spawnerTransform = new List<Transform>();
    [Header("Timing Settings")]
    [SerializeField] private float _baseCooldown = 5f;
    [SerializeField] private float _cooldownDecrease = 1f;
    [SerializeField] private float _baseEnemyAmount = 1f;
    [SerializeField] private float _enemyAmountMultiplier = 1f;
    [SerializeField] private List<string> _seed;
    [Header("Balance")]
    [SerializeField] private float _requiredPlayerDistance;
    
    private float _currentCooldown;
    private float _enemyAmount;
    private List<GameObject> _enemies = new List<GameObject>();
    private Random _random =  new Random();
    private GameObject _enemiesParent;
    private int _totalEnemyWeight;

    [Serializable]
    public class EnemyWeight
    {
        public GameObject prefab;
        public int weight = 1;
    }
    
    private void OnEnable()
    {
        LevelController.onGameOver += DestroyEnemies;
        LevelController.onStartGame += GameStart;
        _totalEnemyWeight =  CountWeight(_enemiesPrefab);
    }

    private void OnDisable()
    {
        LevelController.onGameOver -= DestroyEnemies;
        LevelController.onStartGame -= GameStart;
    }


    private void GameStart()
    {
        if (_spawnerTransform.Count == 0)
        {
            foreach (Transform t in GetComponentsInChildren<Transform>())
            {
                _spawnerTransform.Add(t);
            }
        }
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
        if (LevelController.GameIsRunning)
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
                if (Vector2.Distance(_spawnPosition, PlayerController.Instance.transform.position) < _requiredPlayerDistance)
                { 
                    _spawnPosition = _spawnerTransform[_random.Next(0, _spawnerTransform.Count)].position;
                }

                //GameObject _enemieSelected =  _enemiesPrefab[_random.Next(0, _enemiesPrefab.Count)];
                GameObject _enemieSelected = FindPrefab(_enemiesPrefab, _random.Next(0,_totalEnemyWeight));
                _enemies.Add(InstantiateEnemy(_spawnPosition, _enemieSelected));
                
            }

            _enemyAmount *= _enemyAmountMultiplier;
            _currentCooldown -= _cooldownDecrease;
            
            
            if (_currentCooldown < 0.5f) _currentCooldown = 0.5f ;
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

    private int CountWeight(List<EnemyWeight> enemyPrefab)
    {
        int count = 0;
        foreach (EnemyWeight enemyWeight in enemyPrefab)
        {
            count += enemyWeight.weight;
        }
        return count;
    }

    private GameObject FindPrefab(List<EnemyWeight> enemyPrefab, int weight)
    {
        GameObject prefab = enemyPrefab[0].prefab;
        weight++;
        print(weight);
        foreach (EnemyWeight enemyWeight in enemyPrefab)
        {
            weight -= enemyWeight.weight;
            if (weight <= 0)
            {
                prefab = enemyWeight.prefab;
                
                print(enemyWeight.prefab.name);
                break;
            }
        }
        return prefab;
    }
}
