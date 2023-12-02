using Application.Contexts.GameplayContext.Models;
using Application.Contexts.ProjectContext.Configs;
using DG.Tweening;
using UnityEngine;
using Zenject;

namespace Application.Contexts.GameplayContext.Mediators
{
    public class ExpFollowMediator : MonoBehaviour
    {
        [Inject] private readonly CorvetGameConfig _gameConfig;
        [Inject] private readonly PlayerModel _playerModel;

        [SerializeField] private GameObject _expBody;
        [SerializeField] private GameObject _shadow;
        [SerializeField] private float _duration;
    
        private bool _isCollected;

        private void Start()
        {
            _expBody.transform.DOLocalMoveY(0.05f, _duration).SetLoops(-1, LoopType.Yoyo);
            _shadow.transform.DOScale(new Vector3(0.03f, 0.015f, 0),_duration).SetLoops(-1, LoopType.Yoyo);
        }

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
}
