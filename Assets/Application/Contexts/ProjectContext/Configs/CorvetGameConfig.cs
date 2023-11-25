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
        
        [Header("Collectables")] [SerializeField] private float _distanceThreshold;
        
        [Header("Enemies")] [SerializeField] private float _enemySpeed;
        [SerializeField] private float _enemyStoppingDistance;
        
        [Header("Projectiles")] [SerializeField] private ProjectileConfig[] _projectiles;


        public float PanelDuration => _panelDuration;
        public string Name => _name;
        public int Speed => _speed;
        public float DistanceThreshold => _distanceThreshold;
        public float EnemySpeed => _enemySpeed;
        public float EnemyStoppingDistance => _enemyStoppingDistance;
        public ProjectileConfig[] Projectiles => _projectiles;
    }
}