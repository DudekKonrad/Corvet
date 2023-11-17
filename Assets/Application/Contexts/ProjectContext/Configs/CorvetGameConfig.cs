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

        public float PanelDuration => _panelDuration;
        public string Name => _name;
        public int Speed => _speed;
        public float DistanceThreshold => _distanceThreshold;
    }
}