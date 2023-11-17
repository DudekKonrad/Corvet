using System;
using System.Numerics;
using UnityEngine;
using UnityEngine.Pool;
using Vector3 = UnityEngine.Vector3;

namespace Application.Contexts.GameplayContext.Mediators
{
    public class ProjectileView : MonoBehaviour
    {
        [SerializeField] private float _speed;
        private Rigidbody2D _rigidbody;
        private ObjectPool<ProjectileView> _pool;

        public void Init(Vector3 direction, ObjectPool<ProjectileView> pool)
        {
            transform.forward = direction;
            _pool = pool;
            gameObject.SetActive(true);
        }

        private void Destroy() => _pool.Release(this);
        private void Start()
        {
            _rigidbody = GetComponent<Rigidbody2D>();
        }

        private void FixedUpdate()
        {
            _rigidbody.velocity = transform.forward * _speed;
        }
    }
}