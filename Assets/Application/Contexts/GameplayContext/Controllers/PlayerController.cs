using Application.Contexts.GameplayContext.Models;
using Application.Contexts.ProjectContext.Configs;
using Application.Utils;
using TMPro;
using UnityEngine;
using Zenject;

namespace Application.Contexts.GameplayContext.Controllers
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class PlayerController : MonoBehaviour
    {
        [Inject] private readonly CorvetGameConfig _gameConfig;
        [Inject] private readonly PlayerInputModel _playerInput;
        [Inject] private readonly PlayerModel _playerModel;

        [SerializeField] private FillBar _healthBar;
        [SerializeField] private TMP_Text _healthText;

        private Rigidbody2D _rigidbody;
        private float _timer;

        private void Awake()
        {
            _rigidbody = GetComponent<Rigidbody2D>();
        }

        private void FixedUpdate()
        {
            _rigidbody.velocity = _playerInput.Movement * _gameConfig.Speed;
            _playerModel.SetPlayerTransform(transform);
        }

        private void Update()
        {
            var healthNormalized = _playerModel.CurrentHealth / _playerModel.MaxHealth;
            _healthBar.SetFill(healthNormalized, 0.2f);
            _healthText.text = $"{_playerModel.CurrentHealth} / {_playerModel.MaxHealth}";
        }
    }
}