namespace Application.Contexts.GameplayContext.Models
{
    public class ScoreModel
    {
        private int _score;

        public int Score => _score;

        public void AddScore(int value)
        {
            _score += value;
        }
    }
}