using Application.Contexts.ProjectContext.Configs;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Application.Contexts.GameplayContext
{
    public class EnemyController : MonoBehaviour
    {
        [Inject(Id = nameof(_playerController))] private readonly PlayerController _playerController;
        [Inject] private readonly CorvetGameConfig _gameConfig;
        [Inject] private readonly EnemyModel _enemyModel;

        [SerializeField] private Image _healthBar;
        [SerializeField] private float _duration;
    
        private Rigidbody2D _rigidbody;
        private Color _startingColor;

        private void Start()
        {
            _rigidbody = GetComponent<Rigidbody2D>();
            _enemyModel.CurrentHealthPoints = _enemyModel.MaxHealthPoints;
            _startingColor = _healthBar.color;
        }

        private void FixedUpdate()
        {
            var targetPosition = _playerController.transform.position;
            var currentPosition = transform.position;
            var distance = Vector3.Distance(currentPosition, targetPosition);

            var direction = targetPosition - currentPosition;
            direction.Normalize();
            _rigidbody.velocity = direction * _gameConfig.EnemySpeed;
        }

        public void TakeDamage(int damage)
        {
            _enemyModel.CurrentHealthPoints -= damage;
            var fill = (float)_enemyModel.CurrentHealthPoints / _enemyModel.MaxHealthPoints;
            _healthBar.DOFillAmount(fill, _duration);
            _healthBar.DOColor(new Color(1f, 0.4f, 0.4f, 1f),_duration).OnComplete(() =>
            {
                _healthBar.DOColor(_startingColor, _duration);
            });
            _healthBar.transform.DOScale(Vector3.one*1.1f,_duration).SetLoops(2, LoopType.Yoyo);
        }
    }
}
