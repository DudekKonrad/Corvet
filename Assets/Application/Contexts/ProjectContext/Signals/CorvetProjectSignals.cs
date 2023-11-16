namespace Application.Contexts.ProjectContext.Signals
{
    public class CorvetProjectSignals
    {
        public class ScoreChangedSignal
        {
            private int _score;

            public ScoreChangedSignal(int score)
            {
                _score = score;
            }

            public int Score => _score;
        }
    }
}