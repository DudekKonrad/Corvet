using UnityEngine;

namespace Application.Contexts.GameplayContext.Models
{
    public class PlayerModel
    {
        private Vector3 _playerPosition;
        private int _maxhealth;

        public void SetPlayerPosition(Vector3 position)
        {
            _playerPosition = position;
        }

        public Vector3 PlayerPosition => _playerPosition;
        public int MaxHealth => _maxhealth;
    }
}