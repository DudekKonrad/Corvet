using System.Linq;
using Application.Contexts.ProjectContext.Configs;
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
        
        private Rigidbody2D _rigidbody;
        private ObjectPool<ProjectileView> _pool;
        private Vector3 _direction;
        private ProjectileConfig _projectileConfig;
        
        public void Init(ObjectPool<ProjectileView> pool, Vector3 direction)
        {
            _pool = pool;
            _direction = direction;
            _direction.Normalize();
            gameObject.SetActive(true);
        }

        public void Destroy() => _pool.Release(this);
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
            var enemy = col.gameObject.GetComponent<EnemyController>();
            if (enemy != null)
            {
                enemy.TakeDamage(_projectileConfig.Damage);
            }
            Destroy();
        }
    }
}