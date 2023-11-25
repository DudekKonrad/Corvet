using Application.Contexts.ProjectContext.Configs;
using Zenject;

namespace Application.Contexts.GameplayContext
{
    public class EnemyModel
    {
        [Inject] private readonly CorvetGameConfig _gameConfig;

        [Inject]
        private void Construct()
        {
            _maxHealthPoints = _gameConfig.EnemyMaxHp;
        }

        private int _maxHealthPoints;
        public int MaxHealthPoints => _maxHealthPoints;
        public int CurrentHealthPoints;
    }
}