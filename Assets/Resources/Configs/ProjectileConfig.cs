﻿using System;
using UnityEngine;

namespace Resources.Configs
{
    public enum ProjectileType
    {
        Normal = 0
    }
    
    [Serializable]
    public class ProjectileConfig
    {
        [SerializeField] private ProjectileType _type;
        [SerializeField] private float _projectileSpeed;
        [SerializeField] private int _damage;
        
        public ProjectileType Type => _type;
        public float ProjectileSpeed => _projectileSpeed;
        public int Damage => _damage;
    }
}