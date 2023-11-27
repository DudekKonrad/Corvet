using Resources.Configs;
using UnityEngine;

namespace Application.Contexts.ProjectContext.Configs
{
    [CreateAssetMenu(fileName = "CorvetGameConfig", menuName = "Configs/CorvetGameConfig", order = 1)]
    public class CorvetGameConfig : ScriptableObject
    {
        [Header("UI")] [SerializeField] private float _panelDuration;
        
        [Header("Camera")] [SerializeField] private float _damping;
        public float Damping => _damping;

        [Header("Player")] [SerializeField] private string _name;
        [SerializeField] private int _speed;
        
        [Header("Collectables")] [SerializeField] private float _followSpeed;
        
        [Header("Enemies")] [SerializeField] private float _enemySpeed;
        [SerializeField] private float _enemyStoppingDistance;
        [SerializeField] private float _enemySpawnCooldown;
        [SerializeField] private int _enemyMaxHp;

        [Header("Projectiles")] [SerializeField] private ProjectileConfig[] _projectiles;


        public float PanelDuration => _panelDuration;
        public string Name => _name;
        public int Speed => _speed;
        public float FollowSpeed => _followSpeed;
        public float EnemySpeed => _enemySpeed;
        public float EnemyStoppingDistance => _enemyStoppingDistance;
        public ProjectileConfig[] Projectiles => _projectiles;
        public float EnemySpawnCooldown => _enemySpawnCooldown;
        public int EnemyMaxHp => _enemyMaxHp;
    }
}