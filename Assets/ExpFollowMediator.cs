using Application.Contexts.GameplayContext;
using Application.Contexts.GameplayContext.Models;
using Application.Contexts.ProjectContext.Configs;
using UnityEngine;
using Zenject;

public class ExpFollowMediator : MonoBehaviour
{
    [Inject] private readonly CorvetGameConfig _gameConfig;
    [Inject] private readonly PlayerModel _playerModel;
    
    private bool _isCollected;

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.GetComponent<PlayerController>())
        {
            _isCollected = true;
        }
    }

    private void Update()
    {
        if (_isCollected)
        {
            var step = _gameConfig.FollowSpeed * Time.deltaTime; 
            transform.position = Vector3.MoveTowards(transform.position, _playerModel.PlayerPosition, step);
        }
    }
}
