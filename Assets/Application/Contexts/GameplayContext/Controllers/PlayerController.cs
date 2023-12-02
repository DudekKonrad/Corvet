using System.Collections.Generic;
using System.Linq;
using Application.Contexts.GameplayContext.Mediators;
using Application.Contexts.GameplayContext.Models;
using Application.Contexts.GameplayContext.Services;
using Application.Contexts.ProjectContext.Configs;
using UnityEngine;
using UnityEngine.Pool;
using Zenject;

namespace Application.Contexts.GameplayContext.Controllers
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class PlayerController : MonoBehaviour
    {
        [Inject] private readonly DiContainer _diContainer;
        [Inject] private readonly CorvetGameConfig _gameConfig;
        [Inject] private readonly PlayerInputModel _playerInput;
        [Inject] private readonly PlayerModel _playerModel;
        [Inject(Id = nameof(_enemySpawnerService))] private EnemySpawnerService _enemySpawnerService;
        
        [SerializeField] private ProjectileView _projectilePrefab;

        private ObjectPool<ProjectileView> _pool;
        private Rigidbody2D _rigidbody;
        private float _timer;

        private void Awake()
        {
            _pool = new ObjectPool<ProjectileView>(OnCreateProjectile,OnGetProjectile, OnReleaseProjectile, 
                defaultCapacity: 200);
            _rigidbody = GetComponent<Rigidbody2D>();
        }

        private void OnGetProjectile(ProjectileView obj)
        {
            obj.transform.position = transform.position;
        }

        private void OnReleaseProjectile(ProjectileView obj)
        {
            obj.gameObject.SetActive(false);
        }

        private ProjectileView OnCreateProjectile()
        {
            var projectile = _diContainer.InstantiatePrefabForComponent<ProjectileView>(_projectilePrefab, transform);
            return projectile;
        }

        private void FixedUpdate()
        {
            _rigidbody.velocity = _playerInput.Movement * _gameConfig.Speed;
            _playerModel.SetPlayerPosition(transform.position);
        }

        private void Update()
        {
            _timer += Time.deltaTime;
            if (_timer >= 1f/_gameConfig.FireRate)
            {
                _timer = 0;
                Shoot();
            }
        }
        private void Shoot()
        {
            var enemy = GetClosestEnemy(_enemySpawnerService.Enemies.Where(_ => _.IsActiveInPool).Select(_ => 
                _.GameObject.transform));
            if (enemy != null)
            {
                var projectile = _pool.Get();
                var directionToEnemy = enemy.position - transform.position;
                projectile.Init(_pool, directionToEnemy);
            }
        }
        
        private Transform GetClosestEnemy (IEnumerable<Transform> enemies)
        {
            Transform bestTarget = null;
            var closestDistanceSqr = Mathf.Infinity;
            var currentPosition = transform.position;
            foreach( var potentialTarget in enemies)
            {
                var directionToTarget = potentialTarget.position - currentPosition;
                var dSqrToTarget = directionToTarget.sqrMagnitude;
                if(dSqrToTarget < closestDistanceSqr)
                {
                    closestDistanceSqr = dSqrToTarget;
                    bestTarget = potentialTarget;
                }
            }
            return bestTarget;
        }
    }
}