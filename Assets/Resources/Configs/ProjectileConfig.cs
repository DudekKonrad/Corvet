using System;
using UnityEngine;

namespace Resources.Configs
{
    public enum ProjectileType
    {
        Leaf = 0,
        Dust = 1
    }
    
    [Serializable]
    public class ProjectileConfig
    {
        [SerializeField] private GameObject _prefab;
        [SerializeField] private ProjectileType _type;
        [SerializeField] private float _projectileSpeed;
        [SerializeField] private int _damage;
        [SerializeField] private float _cooldown;
        [SerializeField] private float _destroyDelay;

        public GameObject Prefab => _prefab;
        public ProjectileType Type => _type;
        public float ProjectileSpeed => _projectileSpeed;
        public int Damage => _damage;
        public float Cooldown => _cooldown;
        public float DestroyDelay => _destroyDelay;
    }
}