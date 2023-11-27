namespace Application.Contexts.ProjectContext.Signals
{
    public class CorvetProjectSignals
    {
        public class ExpChangedSignal
        {
            private int _exp;

            public ExpChangedSignal(int exp)
            {
                _exp = exp;
            }

            public int Exp => _exp;
        }
    }
}