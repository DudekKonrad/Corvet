using Application.Contexts.ProjectContext.Signals;
using Zenject;

namespace Application.Contexts.GameplayContext.Models
{
    public class ExpModel
    {
        [Inject] private readonly SignalBus _signalBus;
        
        private int _exp;

        public int Exp => _exp;

        public void AddExp(int value)
        {
            _exp += value;
            _signalBus.Fire(new CorvetProjectSignals.ExpChangedSignal(_exp));
        }
    }
}