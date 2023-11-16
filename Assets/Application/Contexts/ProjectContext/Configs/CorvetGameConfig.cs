using UnityEngine;

namespace Application.Contexts.ProjectContext.Configs
{
    [CreateAssetMenu(fileName = "CorvetGameConfig", menuName = "Configs/CorvetGameConfig", order = 1)]
    public class CorvetGameConfig : ScriptableObject
    {
        [Header("Camera")] [SerializeField] private float _damping;
        public float Damping => _damping;
    }
}