using System.Linq;
using Application.Contexts.GameplayContext.Controllers;
using Application.Contexts.ProjectContext.Configs;
using DG.Tweening;
using Resources.Configs;
using UnityEngine;
using UnityEngine.Pool;
using Zenject;

namespace Application.Contexts.GameplayContext.Mediators
{
    public class ProjectileView : MonoBehaviour
    {
        [Inject] private readonly CorvetGameConfig _gameConfig;

        [SerializeField] private ProjectileType _projectileType;
        [SerializeField] private ParticleSystem _particleSystem;
        [SerializeField] private float _destroyDelay;
        
        private Rigidbody2D _rigidbody;
        private ObjectPool<ProjectileView> _pool;
        private Vector3 _direction;
        private ProjectileConfig _projectileConfig;
        
        public void Init(ObjectPool<ProjectileView> pool, Vector3 direction)
        {
            _pool = pool;   
            _direction = direction;
            _direction.Normalize();
            transform.up = direction;
            gameObject.SetActive(true);
        }

        private float GetLookAngle(Transform target)
        {
            var look = transform.InverseTransformPoint(target.position);
            var angle = Mathf.Atan2(look.y, look.x) * Mathf.Rad2Deg - 90;
            return angle;
        }

        private void Destroy() => _pool.Release(this);
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
            var enemy = col.gameObject.GetComponent<RatEnemyController>();
            if (enemy != null)
            {
                enemy.TakeDamage(_projectileConfig.Damage);
                _particleSystem.Play();
            }
            DOVirtual.DelayedCall(_destroyDelay, Destroy);
        }
    }
}