using System.Collections.Generic;
using Application.Contexts.GameplayContext.Controllers;
using Application.Contexts.GameplayContext.Models;
using Application.Contexts.ProjectContext.Configs;
using Application.Utils;
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
        
        [SerializeField] private GameObject _spawnMarker;
        
        private ObjectPool<IEnemy> _enemiesPool;
        private List<IEnemy> _enemies = new List<IEnemy>();
        private float _spawnCooldown;
        private Vector3 _spawnPosition;
        public List<IEnemy> Enemies => _enemies;
        
        private void Start()
        {
            _enemiesPool = new ObjectPool<IEnemy>(OnCreateEnemy, OnGetEnemy, OnReleaseEnemy, defaultCapacity: 100);
            _spawnCooldown = _gameConfig.EnemiesDict[EnemyType.Worm].SpawnCooldown;
            _spawnPosition = PositionUtils.RandomPositionInRange(-9f, 9f, -9f, 9f);
            _spawnMarker.transform.position = _spawnPosition;
        }

        private WormEnemyController OnCreateEnemy()
        {
            var enemy = _diContainer.InstantiatePrefabForComponent<WormEnemyController>(
                _gameConfig.EnemiesDict[EnemyType.Worm].EnemyPrefab, _enemiesContainer);
            _enemies.Add(enemy);
            return enemy;
        }

        private void Update()
        {
            Timer.ProcessTime(ref _spawnCooldown, Time.deltaTime);
            if (_spawnCooldown <= 0)
            {
                SpawnEnemy(_enemiesPool.Get());
            }
        }

        private void SpawnEnemy(IEnemy enemy)
        {
            enemy.Init(_enemiesPool);
            enemy.GameObject.transform.position = _spawnPosition;
            _spawnCooldown = _gameConfig.EnemiesDict[enemy.EnemyType].SpawnCooldown;
            _spawnPosition = PositionUtils.RandomPositionInRange(-9f, 9f, -9f, 9f);
            _spawnMarker.transform.position = _spawnPosition;
        }

        private void OnReleaseEnemy(IEnemy enemy)
        {
            enemy.IsActiveInPool = false;
            enemy.GameObject.SetActive(false);
        }

        private void OnGetEnemy(IEnemy enemy)
        {
            enemy.GameObject.transform.localPosition = Vector3.zero;
            enemy.IsActiveInPool = true;
            enemy.GameObject.SetActive(true);
        }
    }
}