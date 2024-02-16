using Application.Contexts.GameplayContext.Models;
using AYellowpaper.SerializedCollections;
using Resources.Configs;
using UnityEngine;

namespace Application.Contexts.ProjectContext.Configs
{
    [CreateAssetMenu(fileName = "CorvetGameConfig", menuName = "Configs/CorvetGameConfig", order = 1)]
    public class CorvetGameConfig : ScriptableObject
    {
        [Header("UI")] [SerializeField] private float _panelDuration;

        [Header("Player")] [SerializeField] private float _maxHealth;
        [SerializeField] private int _speed;
        [SerializeField] private float _fireRate;

        [Header("Collectables")] [SerializedDictionary("Exp Type", "Exp Config")] [SerializeField]
        private SerializedDictionary<ExpType, ExpConfig> _expDictionary;

        [SerializedDictionary("Level", "Threshold")] [SerializeField]
        private SerializedDictionary<int, int> _levelProgress;

        [Header("Enemies")] [SerializedDictionary("Enemy Type", "Enemy Config")] [SerializeField]
        private SerializedDictionary<EnemyType, EnemyConfig> _enemiesDict;

        [Header("Projectiles")] [SerializedDictionary("Projectile Type", "Projectile Config")] [SerializeField]
        private SerializedDictionary<ProjectileType, ProjectileConfig> _projectiles;


        public float PanelDuration => _panelDuration;
        public float MaxHealth => _maxHealth;
        public int Speed => _speed;
        public float FireRate => _fireRate;
        public SerializedDictionary<EnemyType, EnemyConfig> EnemiesDict => _enemiesDict;
        public SerializedDictionary<ExpType, ExpConfig> ExpDict => _expDictionary;
        public SerializedDictionary<int, int> LevelProgress => _levelProgress;
        public SerializedDictionary<ProjectileType, ProjectileConfig> Projectiles => _projectiles;
    }
}