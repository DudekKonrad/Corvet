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


        private ObjectPool<RatEnemyController> _enemiesPool;
        private List<RatEnemyController> _enemies = new List<RatEnemyController>();
        private float _spawnCooldown;
        public List<RatEnemyController> Enemies => _enemies;
        
        private void Start()
        {
            _enemiesPool = new ObjectPool<RatEnemyController>(OnCreateEnemy, OnGetEnemy, OnReleaseEnemy, 
                defaultCapacity: 100);
            _spawnCooldown = _gameConfig.EnemiesDict[EnemyType.Rat].SpawnCooldown;  
        }

        private RatEnemyController OnCreateEnemy()
        {
            var enemy = _diContainer.InstantiatePrefabForComponent<RatEnemyController>(
                _gameConfig.EnemiesDict[EnemyType.Rat].EnemyPrefab, _enemiesContainer);
            _enemies.Add(enemy);
            return enemy;
        }

        private void Update()
        {
            Timer.ProcessTime(ref _spawnCooldown, Time.deltaTime);
            if (_spawnCooldown <= 0)
            {
                var enemy = _enemiesPool.Get();
                enemy.Init(_enemiesPool);
                _spawnCooldown = _gameConfig.EnemiesDict[EnemyType.Rat].SpawnCooldown;
            }
        }

        private void OnReleaseEnemy(RatEnemyController ratEnemy)
        {
            ratEnemy.IsActiveInPool = false;
            ratEnemy.gameObject.SetActive(false);
        }

        private void OnGetEnemy(RatEnemyController ratEnemy)
        {
            ratEnemy.transform.localPosition = Vector3.zero;
            ratEnemy.IsActiveInPool = true; 
            ratEnemy.gameObject.SetActive(true);
        }
    }
}