using Application.Contexts.GameplayContext.Models;
using UnityEngine;
using Zenject;

namespace Application.Contexts.GameplayContext
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class PlayerController : MonoBehaviour
    {
        [Inject] private readonly PlayerInputModel _playerInput;
        
        [SerializeField] private Rigidbody2D _rigidbody;
        [SerializeField] private float _speed;

        private void FixedUpdate()
        {
            _rigidbody.velocity = _playerInput.Movement * _speed;
        }
    }
}