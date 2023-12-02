using Application.Contexts.ProjectContext.Configs;
using Zenject;

namespace Application.Contexts.GameplayContext
{
    public enum EnemyType
    {
        Rat = 0
    }
    
    public class EnemyModel
    {
        [Inject] private readonly CorvetGameConfig _gameConfig;

        [Inject]
        private void Construct()
        {
            _maxHealthPoints = _gameConfig.EnemiesDict[EnemyType.Rat].MaxHealthPoints;
        }

        private int _maxHealthPoints;
        public int MaxHealthPoints => _maxHealthPoints;
        public int CurrentHealthPoints;
    }
}