using System.Linq;
using Application.Contexts.GameplayContext.Controllers;
using Application.Contexts.GameplayContext.Services;
using Application.Contexts.ProjectContext.Configs;
using DG.Tweening;
using Resources.Configs;
using UnityEngine;
using Zenject;

namespace Application.Contexts.GameplayContext.Mediators
{
    public class LeafProjectileView : MonoBehaviour, IProjectile
    {
        [Inject] private readonly CorvetGameConfig _gameConfig;
        [Inject] private readonly ProjectilesService _projectilesService;

        [SerializeField] private ProjectileType _projectileType;
        [SerializeField] private ParticleSystem _particleSystem;
        [SerializeField] private float _destroyDelay;
        
        private Rigidbody2D _rigidbody;
        private Vector3 _direction;
        private ProjectileConfig _projectileConfig;
        public Transform Transform => transform;
        public GameObject GameObject => gameObject;



        public void Init(Vector3 direction)
        {
            _direction = direction;
            _direction.Normalize();
            transform.up = direction;
            gameObject.SetActive(true);
        }

        private void Destroy() => _projectilesService.Pool.Release(this);
        
        private void Start()
        {
            _rigidbody = GetComponent<Rigidbody2D>();
            _projectileConfig = _gameConfig.Projectiles.First(_ => _.Type == _projectileType);
        }

        private void FixedUpdate()
        {
            _rigidbody.velocity = _direction * _projectileConfig.ProjectileSpeed;
        }

        private void OnCollisionEnter2D(Collision2D col)
        {
            var enemy = col.gameObject.GetComponent<WormEnemyController>();
            if (enemy != null)
            {
                enemy.TakeDamage(_projectileConfig.Damage);
                _particleSystem.Play();
            }
            DOVirtual.DelayedCall(_destroyDelay, Destroy);
        }
    }
}