using Application.Contexts.GameplayContext.Models;
using Application.Contexts.ProjectContext.Configs;
using DG.Tweening;
using UnityEngine;
using Zenject;

namespace Application.Contexts.GameplayContext.Mediators
{
    public class ExpMediator : MonoBehaviour, ICollectable
    {
        [Inject] private readonly ExpModel _expModel;
        [Inject] private readonly CorvetGameConfig _gameConfig;
        [Inject] private readonly PlayerModel _playerModel;
        
        [SerializeField] private float _duration;
        [SerializeField] private int _value = 5;
        
        private bool _collected;
        
        public bool IsActiveInPool;

        private void OnTriggerEnter2D(Collider2D col)
        {
            if (col.gameObject.GetComponent<PlayerController>())
            {
                transform.DOScale(Vector3.one * 0.1f, _duration);
                transform.DOMove(_playerModel.PlayerPosition, _duration).SetEase(Ease.InSine).OnComplete(() =>
                {
                    _expModel.AddExp(_value);
                    transform.parent.gameObject.SetActive(false);
                });
            }
        }

        public void Collect()
        {
            _collected = true;
        }
    }
}