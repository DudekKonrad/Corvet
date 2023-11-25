using Application.Contexts.ProjectContext.Configs;
using UnityEngine;
using UnityEngine.Pool;
using Zenject;

namespace Application.Contexts.GameplayContext.Mediators
{
    public class ProjectileView : MonoBehaviour
    {
        [Inject] private readonly CorvetGameConfig _gameConfig;
        
        private Rigidbody2D _rigidbody;
        private ObjectPool<ProjectileView> _pool;
        private Vector3 _direction;

        public void Init(ObjectPool<ProjectileView> pool, Vector3 direction)
        {
            _pool = pool;
            _direction = direction;
            gameObject.SetActive(true);
        }

        public void Destroy() => _pool.Release(this);
        private void Start()
        {
            _rigidbody = GetComponent<Rigidbody2D>();
        }

        private void FixedUpdate()
        {
            _rigidbody.velocity = _direction * _gameConfig.ProjectileSpeed;
        }

        private void OnCollisionEnter2D(Collision2D col)
        {
            var enemy = col.gameObject.GetComponent<EnemyController>();
            if (enemy != null)
            {
                Debug.Log($"Projectile hit enemy!");
                Destroy();
            }
        }
    }
}