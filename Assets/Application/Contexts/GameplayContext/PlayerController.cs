using Application.Contexts.GameplayContext.Mediators;
using Application.Contexts.GameplayContext.Models;
using Application.Contexts.ProjectContext.Configs;
using UnityEngine;
using UnityEngine.Pool;
using Zenject;

namespace Application.Contexts.GameplayContext
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class PlayerController : MonoBehaviour
    {
        [Inject] private readonly DiContainer _diContainer;
        [Inject] private readonly CorvetGameConfig _gameConfig;
        [Inject] private readonly PlayerInputModel _playerInput;
        [Inject] private readonly PlayerModel _playerModel;

        [SerializeField] private Rigidbody2D _rigidbody;
        [SerializeField] private float _fireRate;
        [SerializeField] private ProjectileView _projectilePrefab;
        [SerializeField] private Transform[] _enemies;
        [SerializeField] private ProjectileView[] _projectilesContainer;

        private ObjectPool<ProjectileView> _pool;

        private float _timer;

        private void Awake()
        {
            _pool = new ObjectPool<ProjectileView>(CreateProjectile,OnGetProjectile, OnPutBackInPool, 
                defaultCapacity: 200);
        }

        private void OnGetProjectile(ProjectileView obj)
        {
            obj.transform.position = transform.position;
        }

        private void OnPutBackInPool(ProjectileView obj)
        {
            obj.gameObject.SetActive(false);
        }

        private ProjectileView CreateProjectile()
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
            if (_timer >= 1f/_fireRate)
            {
                _timer = 0;
                Shoot();
            }
        }
        private void Shoot()
        {
            var projectile = _pool.Get();
            var directionToEnemy = GetClosestEnemy(_enemies).position - transform.position;
            projectile.Init(_pool, directionToEnemy);
        }
        
        private Transform GetClosestEnemy (Transform[] enemies)
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