using System;
using UnityEngine;

namespace Application.Contexts.ProjectContext.Configs
{
    [Serializable]
    public class EnemyConfig
    { 
        [SerializeField] private GameObject _enemyPrefab;
        [SerializeField] private int _maxHealthPoints;
        [SerializeField] private float _spawnCooldown;
        [SerializeField] private float _movementSpeed;

        public GameObject EnemyPrefab => _enemyPrefab;
        public int MaxHealthPoints => _maxHealthPoints;
        public float SpawnCooldown => _spawnCooldown;
        public float MovementSpeed => _movementSpeed;
    }
}