using Application.Contexts.GameplayContext.Models;
using UnityEngine;
using Zenject;

namespace Application.Contexts.GameplayContext.Mediators
{
    public class MovingParticlesMediator : MonoBehaviour
    {
        [Inject] private readonly PlayerInputModel _playerModel;
        
        [SerializeField] private ParticleSystem _leftParticleSystem;
        [SerializeField] private ParticleSystem _rightParticleSystem;
        

        private void Update()
        {
            var playerMovingLeft = _playerModel.Movement.x < 0;
            var playerMovingRight = _playerModel.Movement.x > 0;
            var playerStanding = _playerModel.Movement == Vector2.zero;
            if (playerMovingRight)
            {
                _leftParticleSystem.Play();
                _rightParticleSystem.Stop();
            }

            if (playerMovingLeft)
            {
                _rightParticleSystem.Play();
                _leftParticleSystem.Stop();
            }

            if (playerStanding)
            {
                _leftParticleSystem.Stop();
                _rightParticleSystem.Stop();
            }
        }
    }
}
