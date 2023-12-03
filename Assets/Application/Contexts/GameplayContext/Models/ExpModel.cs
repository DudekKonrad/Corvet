using Application.Contexts.ProjectContext.Configs;
using Application.Contexts.ProjectContext.Signals;
using Zenject;

namespace Application.Contexts.GameplayContext.Models
{
    public class ExpModel
    {
        [Inject] private readonly SignalBus _signalBus;
        [Inject] private readonly CorvetGameConfig _gameConfig;
        
        private int _exp;
        private int _level = 1;

        public int Level => _level;
        public int Exp => _exp;

        public void AddExp(int value)
        {
            _exp += value;
            _signalBus.Fire(new CorvetProjectSignals.ExpChangedSignal(_exp));
            for (var i = _level; i < _gameConfig.LevelProgress.Count; i++)
            {
                if (_exp >= _gameConfig.LevelProgress[i])
                {
                    _level++;
                    _signalBus.Fire(new CorvetProjectSignals.LevelUpSignal());
                }
            }
        }
    }
}