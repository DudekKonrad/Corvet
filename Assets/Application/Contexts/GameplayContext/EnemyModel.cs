namespace Application.Contexts.GameplayContext
{
    public class EnemyModel
    {
        public int CurrentHealthPoints;
        private int _maxHealthPoints = 50;

        public int MaxHealthPoints => _maxHealthPoints;
    }
}