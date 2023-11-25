using System;
using Application.Contexts.GameplayContext.Models;
using Application.Contexts.ProjectContext.Configs;
using UnityEngine;
using Zenject;

namespace Application.Contexts.GameplayContext.Mediators
{
    public class CoinMediator : MonoBehaviour, ICollectable
    {
        [Inject] private readonly CorvetGameConfig _gameConfig;
        [Inject] private readonly ScoreModel _scoreModel;
        [Inject] private readonly PlayerModel _playerModel;

        [SerializeField] private int _value = 5;
        [SerializeField] private int _speed = 5;
        
        private CircleCollider2D _collider;
        private bool _collected;

        private void Start()
        {
            _collider = GetComponent<CircleCollider2D>();
        }

        private void OnTriggerEnter2D(Collider2D col)
        {
            Debug.Log($"Coin trigger!");
            if (col.gameObject.GetComponent<PlayerController>())
            { 
                _scoreModel.AddScore(_value);
                gameObject.SetActive(false);
            }
        }

        private void Update()
        {
            if (_collected)
            {
                _collider.enabled = false;
                var step = _speed * Time.deltaTime;
                transform.position = Vector3.MoveTowards(transform.position, _playerModel.PlayerPosition, step);
                if (Vector3.Distance(transform.position, _playerModel.PlayerPosition) <= _gameConfig.DistanceThreshold)
                {
                    Destroy(gameObject);
                }
            }
        }

        public void Collect()
        {
            _collected = true;
        }
    }
}