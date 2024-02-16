using Application.Contexts.GameplayContext.Models;
using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.InputSystem;
using Zenject;

namespace Application.Contexts.GameplayContext.Mediators
{
    public class PlayerInputMediator : MonoBehaviour
    {
        [Inject] private PlayerInputModel _playerInputModel;

        [UsedImplicitly]
        private void OnPoint(InputValue value)
        {
            var inputValue = value.Get<Vector2>();
            _playerInputModel.Point = inputValue;
        }

        [UsedImplicitly]
        private void OnMovement(InputValue value)
        {
            var inputValue = value.Get<Vector2>();
            _playerInputModel.Movement = inputValue;
        }

        [UsedImplicitly]
        private void OnSpaceClick(InputValue value)
        {
            _playerInputModel.SpaceClick = value.isPressed;
        }
    }
}