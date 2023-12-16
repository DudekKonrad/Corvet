using System.Collections.Generic;
using System.Linq;
using Application.Contexts.GameplayContext.Mediators;
using Application.Contexts.GameplayContext.Models;
using Application.Contexts.ProjectContext.Configs;
using Application.Utils;
using Resources.Configs;
using UnityEngine;
using UnityEngine.Pool;
using Zenject;

namespace Application.Contexts.GameplayContext.Services
{
    public class ProjectilesService : ITickable
    {
        [Inject] private readonly DiContainer _diContainer;
        [Inject] private readonly CorvetGameConfig _gameConfig;
        [Inject] private readonly PlayerModel _playerModel;
        [Inject(Id = nameof(_enemySpawnerService))] private EnemySpawnerService _enemySpawnerService;

        
        private Dictionary<ProjectileType, ObjectPool<IProjectile>> _poolDictionary = 
            new Dictionary<ProjectileType, ObjectPool<IProjectile>>();
        private Dictionary<ProjectileType, CooldownTimer<ProjectileType>> _timersDictionary = 
            new Dictionary<ProjectileType, CooldownTimer<ProjectileType>>();
        
        public Dictionary<ProjectileType, ObjectPool<IProjectile>> PoolDictionary => _poolDictionary;
        public Dictionary<ProjectileType, CooldownTimer<ProjectileType>> TimersDictionary => _timersDictionary;

        private GameObject GetProjectilePrefab(ProjectileType projectileType) => 
            _gameConfig.Projectiles[projectileType].Prefab;

        [Inject]
        private void Construct()
        {
            foreach (var projectile in _playerModel.ActiveProjectiles)
            {
                var pool = new ObjectPool<IProjectile>(() => OnCreateProjectile(projectile),
                    OnGetProjectile, OnReleaseProjectile);
                _poolDictionary.Add(projectile, pool);
                var cooldown = _gameConfig.Projectiles[projectile].Cooldown;
                _timersDictionary.Add(projectile,
                    new CooldownTimer<ProjectileType>(cooldown, ShootInDirection, projectile));
            }
        }

        private void ShootInDirection(ProjectileType projectileType)
        {
            var enemies = _enemySpawnerService.Enemies.Where(_ => _.IsActiveInPool).Select(_ => 
                _.GameObject.transform);
            var closestEnemy = _playerModel.Transform.GetClosestTransform(enemies);
            if (closestEnemy)
            {
                var directionToEnemy = closestEnemy.position - _playerModel.Position;
                var projectile = GetProjectile(projectileType);
                projectile.Init(directionToEnemy);
            }
        }

        public IProjectile GetProjectile(ProjectileType projectileType)
        {
            return _poolDictionary[projectileType].Get();
        }
        
        private void OnGetProjectile(IProjectile projectile)
        {
            projectile.Transform.position = _playerModel.Position;
        }

        private void OnReleaseProjectile(IProjectile projectile)
        {
            projectile.GameObject.SetActive(false);
        }

        private IProjectile OnCreateProjectile(ProjectileType projectileType)
        {
            var projectile = _diContainer.InstantiatePrefabForComponent<IProjectile>(GetProjectilePrefab(projectileType), 
                _playerModel.Transform);
            return projectile;
        }

        public void Tick()
        {
            foreach (var timer in _timersDictionary.Values)
            {
                timer.UpdateTimer(Time.deltaTime);
            }
        }
    }
}