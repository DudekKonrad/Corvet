using System;
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

        private void Update()
        {
            _rigidbody.AddForce(_playerInput.Movement);
        }
    }
}