using System.Collections.Generic;
using Application.Contexts.ProjectContext.Configs;
using Application.Utils.TimeUtils;
using UnityEngine;
using UnityEngine.Pool;
using Zenject;

namespace Application.Contexts.GameplayContext.Services
{
    public class EnemySpawnerService : MonoBehaviour
    {
        [Inject(Id = nameof(_enemiesContainer))] private readonly Transform _enemiesContainer;
        [Inject] private readonly DiContainer _diContainer;
        [Inject] private readonly CorvetGameConfig _gameConfig;

        [SerializeField] private EnemyController _enemyPrefab;

        private ObjectPool<EnemyController> _enemiesPool;
        private List<EnemyController> _enemies = new List<EnemyController>();
        [SerializeField] private float _spawnCooldown;

        public ObjectPool<EnemyController> EnemiesPool => _enemiesPool;
        public List<EnemyController> Enemies => _enemies;

        private void Start()
        {
            _enemiesPool = new ObjectPool<EnemyController>(OnCreateEnemy, OnGetEnemy, OnReleaseEnemy, 
                defaultCapacity: 100);
            _spawnCooldown = _gameConfig.EnemySpawnCooldown;
        }

        private EnemyController OnCreateEnemy()
        {
            var enemy = _diContainer.InstantiatePrefabForComponent<EnemyController>(_enemyPrefab, _enemiesContainer);
            _enemies.Add(enemy);
            return enemy;
        }

        private void Update()
        {
            Timer.ProcessTime(ref _spawnCooldown, Time.deltaTime);
            if (_spawnCooldown <= 0)
            {
                Debug.Log($"Will spawn enemy");
                var enemy = _enemiesPool.Get();
                enemy.Init(_enemiesPool);
                _spawnCooldown = _gameConfig.EnemySpawnCooldown;
            }
        }

        private void OnReleaseEnemy(EnemyController enemy)
        {
            enemy.IsActiveInPool = false;
            enemy.gameObject.SetActive(false);
        }

        private void OnGetEnemy(EnemyController enemy)
        {
            enemy.transform.localPosition = Vector3.zero;
            enemy.IsActiveInPool = true; 
            enemy.gameObject.SetActive(true);
        }
    }
}