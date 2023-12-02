using Application.Contexts.ProjectContext.Configs;
using DG.Tweening;
using UnityEngine;
using UnityEngine.Pool;
using UnityEngine.UI;
using Zenject;

namespace Application.Contexts.GameplayContext
{
    public class RatEnemyController : MonoBehaviour
    {
        [Inject(Id = nameof(_playerController))] private readonly PlayerController _playerController;
        [Inject] private readonly CorvetGameConfig _gameConfig;
        [Inject] private readonly EnemyModel _enemyModel;

        [SerializeField] private Image _healthBar;
        [SerializeField] private float _duration;
        [SerializeField] private ParticleSystem _particleSystem;
        [SerializeField] private SpriteRenderer _blink;
    
        private Rigidbody2D _rigidbody;
        private Color _startingColor;

        public bool IsActiveInPool;
        private ObjectPool<RatEnemyController> _pool;

        private void Start()
        {
            _rigidbody = GetComponent<Rigidbody2D>();
            _enemyModel.CurrentHealthPoints = _enemyModel.MaxHealthPoints;
            _startingColor = _healthBar.color;
        }

        public void Init(ObjectPool<RatEnemyController> pool)
        {
            _pool = pool;
            _enemyModel.CurrentHealthPoints = _gameConfig.EnemiesDict[EnemyType.Rat].MaxHealthPoints;
            var fill = (float)_enemyModel.CurrentHealthPoints / _enemyModel.MaxHealthPoints;
            _healthBar.fillAmount = fill;
        }

        private void FixedUpdate()
        {
            var targetPosition = _playerController.transform.position;
            var currentPosition = transform.position;
            var direction = targetPosition - currentPosition;
            direction.Normalize();
            _rigidbody.velocity = direction * _gameConfig.EnemiesDict[EnemyType.Rat].MovementSpeed;
        }

        private void Blink()
        {
            _blink.gameObject.SetActive(true);
            _blink.color = new Color(1, 1, 1, 1);
            _blink.DOColor(new Color(1, 1, 1, 0), _duration).SetLoops(1, LoopType.Yoyo).
                OnComplete(() => _blink.gameObject.SetActive(false));
        }
        
        public void TakeDamage(int damage)
        {
            _enemyModel.CurrentHealthPoints -= damage;
            var fill = (float)_enemyModel.CurrentHealthPoints / _enemyModel.MaxHealthPoints;
            _particleSystem.Play();
            Blink();
            _healthBar.DOFillAmount(fill, _duration);
            _healthBar.DOColor(new Color(1f, 0.4f, 0.4f, 1f),_duration).OnComplete(() =>
            {
                _healthBar.DOColor(_startingColor, _duration);
                if (_enemyModel.CurrentHealthPoints <= 0)
                {
                    Die();
                }
            });
        }

        private void Die()
        {
            _pool.Release(this);
        }
    }
}
