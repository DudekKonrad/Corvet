using System.Collections.Generic;
using Application.Contexts.ProjectContext.Configs;
using Resources.Configs;
using UnityEngine;
using Zenject;

namespace Application.Contexts.GameplayContext.Models
{
    public class PlayerModel
    {
        [Inject] private readonly CorvetGameConfig _gameConfig;
        
        private Transform _transform;
        private float _maxHealth;
        private float _currentHealth;

        public float CurrentHealth
        {
            get => _currentHealth;
            set
            {
                _currentHealth = value;
                if (_currentHealth <= 0)
                {
                    _currentHealth = 0;
                }

                if (_currentHealth > _maxHealth)
                {
                    _currentHealth = _maxHealth;
                }
            }
        }

        private readonly List<ProjectileType> _activeProjectiles =
            new List<ProjectileType> {ProjectileType.Leaf, ProjectileType.Dust};

        [Inject]
        private void Construct()
        {
            _maxHealth = _gameConfig.MaxHealth;
            CurrentHealth = _maxHealth;
        }

        public void SetPlayerTransform(Transform transform)
        {
            _transform = transform;
        }

        public Transform Transform => _transform;
        public Vector3 Position => _transform.position;
        public float MaxHealth => _maxHealth;
        public List<ProjectileType> ActiveProjectiles => _activeProjectiles;
    }
}