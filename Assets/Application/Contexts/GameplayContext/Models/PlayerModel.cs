using UnityEngine;

namespace Application.Contexts.GameplayContext.Models
{
    public class PlayerModel
    {
        private Vector3 _playerPosition;

        public void SetPlayerPosition(Vector3 position)
        {
            _playerPosition = position;
        }

        public Vector3 PlayerPosition => _playerPosition;
    }
}