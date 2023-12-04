using Application.Contexts.GameplayContext.Models;
using Application.Contexts.GameplayContext.Services;
using Application.Contexts.ProjectContext.Configs;
using Application.Utils;
using DG.Tweening;
using UnityEngine;
using UnityEngine.Pool;
using Zenject;

namespace Application.Contexts.GameplayContext.Controllers
{
    public class WormEnemyController : MonoBehaviour, IEnemy
    {
        [Inject(Id = nameof(_playerController))] private readonly PlayerController _playerController;
        [Inject] private readonly ExpSpawnerService _expSpawnerService;
        [Inject] private readonly CorvetGameConfig _gameConfig;
        [Inject] private readonly EnemyModel _enemyModel;

        [SerializeField] private FillBar _healthBar;
        [SerializeField] private float _duration;
        [SerializeField] private SpriteRenderer _blink;
    
        private Rigidbody2D _rigidbody;
        private ObjectPool<IEnemy> _pool;

        public GameObject GameObject => gameObject;
        public EnemyType EnemyType => EnemyType.Rat;
        public bool IsActiveInPool { get; set; }
        
        private void Start()
        {
            _rigidbody = GetComponent<Rigidbody2D>();
            _enemyModel.CurrentHealthPoints = _enemyModel.MaxHealthPoints;
        }

        public void Init(ObjectPool<IEnemy> enemiesPool)
        {
            _pool = enemiesPool;
            _enemyModel.CurrentHealthPoints = _gameConfig.EnemiesDict[EnemyType].MaxHealthPoints;
            var fill = (float)_enemyModel.CurrentHealthPoints / _enemyModel.MaxHealthPoints;
            _healthBar.Fill.fillAmount = fill;
        }

        private void FixedUpdate()
        {
            Follow(_playerController.transform.position);
        }

        private void Follow(Vector3 target)
        {
            var direction = target - transform.position;
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
            Blink();
            _healthBar.SetFill(fill, _duration, Color.yellow).OnComplete(() =>
            {
                if (_enemyModel.CurrentHealthPoints <= 0)
                {
                    Die();
                }
            });
        }

        private void Die()
        {
            var expPoint = _expSpawnerService.ExpPool.Get();
            expPoint.transform.position = transform.position;
            _pool.Release(this);
        }
    }
}
