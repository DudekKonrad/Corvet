using UnityEngine;

namespace Application.Contexts.GameplayContext.Models
{
    public class PlayerModel
    {
        private Transform _transform;
        private int _maxHealth;
        private int _currentHealth;

        public void SetPlayerTransform(Transform transform)
        {
            _transform = transform;
        }

        public Transform Transform => _transform;
        public Vector3 Position => _transform.position;
        public int MaxHealth => _maxHealth;
        public int CurrentHealth => _currentHealth;
    }
}