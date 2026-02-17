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
    [SerializeField] private float _enemyAmount = 1f;
    [SerializeField] private float _enemyAmountMultiplier = 1f;
    [SerializeField] private string _seed = "beep boop";
    
    private float _currentCooldown;
    private List<GameObject> _enemies = new List<GameObject>();
    private Random _random;
    private GameObject _enemiesParent;

    private void Start()
    {
        _random = new Random(_seed.GetHashCode());
        _enemiesParent = new GameObject();
        _enemiesParent.transform.SetParent(transform);
        _enemiesParent.name = "Enemies";
    }
    
    private void Update()
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

    public void DestroyEnemies()
    {
        foreach(GameObject gameObject in  _enemies) 
        {
            Destroy(gameObject);
        }
        _enemies.Clear();
    }
}
