using Application.Contexts.GameplayContext.Models;
using Application.Contexts.ProjectContext.Configs;
using UnityEngine;
using Zenject;

namespace Application.Contexts.GameplayContext
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class PlayerController : MonoBehaviour
    {
        [Inject] private readonly CorvetGameConfig _gameConfig;
        [Inject] private readonly PlayerInputModel _playerInput;
        [Inject] private readonly PlayerModel _playerModel;

        [SerializeField] private Rigidbody2D _rigidbody;

        private void FixedUpdate()
        {
            _rigidbody.velocity = _playerInput.Movement * _gameConfig.Speed;
            _playerModel.SetPlayerPosition(transform.position);
        }
    }
}