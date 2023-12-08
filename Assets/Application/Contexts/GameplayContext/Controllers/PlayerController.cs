using System.Collections.Generic;
using System.Linq;
using Application.Contexts.GameplayContext.Models;
using Application.Contexts.GameplayContext.Services;
using Application.Contexts.ProjectContext.Configs;
using UnityEngine;
using Zenject;

namespace Application.Contexts.GameplayContext.Controllers
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class PlayerController : MonoBehaviour
    {
        [Inject] private readonly CorvetGameConfig _gameConfig;
        [Inject] private readonly PlayerInputModel _playerInput;
        [Inject] private readonly PlayerModel _playerModel;
        [Inject] private readonly ProjectilesService _projectilesService;
        [Inject(Id = nameof(_enemySpawnerService))] private EnemySpawnerService _enemySpawnerService;
        
        private Rigidbody2D _rigidbody;
        private float _timer;

        private void Awake()
        {
            _rigidbody = GetComponent<Rigidbody2D>();
        }

        private void FixedUpdate()
        {
            _rigidbody.velocity = _playerInput.Movement * _gameConfig.Speed;
            _playerModel.SetPlayerTransform(transform);
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
                var projectile = _projectilesService.GetProjectile();
                var directionToEnemy = enemy.position - transform.position;
                projectile.Init(directionToEnemy);
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