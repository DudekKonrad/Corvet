using Application.Contexts.ProjectContext.Signals;
using Zenject;

namespace Application.Contexts.GameplayContext.Models
{
    public class ScoreModel
    {
        [Inject] private readonly SignalBus _signalBus;
        
        private int _score;

        public int Score => _score;

        public void AddScore(int value)
        {
            _score += value;
            _signalBus.Fire(new CorvetProjectSignals.ScoreChangedSignal(_score));
        }
    }
}