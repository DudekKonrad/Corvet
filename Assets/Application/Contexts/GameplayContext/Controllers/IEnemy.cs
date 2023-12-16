﻿using Application.Contexts.GameplayContext.Models;
using UnityEngine;
using UnityEngine.Pool;

namespace Application.Contexts.GameplayContext.Controllers
{
    public interface IEnemy
    {
        public GameObject GameObject { get; }
        public EnemyType EnemyType { get; }
        public bool IsActiveInPool { get; set; }
        public void TakeDamage(int damageAmount);
        public void DealDamage(int damageAmount);
        void Init(ObjectPool<IEnemy> enemiesPool);
    }
}