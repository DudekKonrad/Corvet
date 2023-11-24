using Application.Contexts.GameplayContext;
using Application.Contexts.ProjectContext.Configs;
using UnityEngine;
using Zenject;

public class EnemyController : MonoBehaviour
{
    [Inject(Id = nameof(_playerController))] private readonly PlayerController _playerController;
    [Inject] private readonly CorvetGameConfig _gameConfig;
    
    private Rigidbody2D _rigidbody;

    private void Start()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
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
}
