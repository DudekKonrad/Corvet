using Application.Contexts.GameplayContext.Controllers;
using Application.Contexts.GameplayContext.Models;
using Application.Contexts.ProjectContext.Configs;
using DG.Tweening;
using UnityEngine;
using UnityEngine.Pool;
using Zenject;

namespace Application.Contexts.GameplayContext.Mediators
{
    public class ExpMediator : MonoBehaviour
    {
        [Inject] private readonly CorvetGameConfig _gameConfig;
        [Inject] private readonly PlayerModel _playerModel;
        [Inject] private readonly ExpModel _expModel;
        
        [SerializeField] private float _duration;
        [SerializeField] private ExpType _expType = ExpType.Medium;
        [SerializeField] private GameObject _expBody;
        [SerializeField] private GameObject _shadow;

        private ObjectPool<ExpMediator> _pool;

        public bool IsActiveInPool;
        public bool IsCollected;

        private void Start()
        {
            SetAnim();
        }

        public void Init(ObjectPool<ExpMediator> pool)
        {
            _pool = pool;
        }

        public void SetAnim()
        {
            _expBody.transform.DOLocalMoveY(0.05f, _duration).SetLoops(-1, LoopType.Yoyo);
            _shadow.transform.DOScale(new Vector3(0.3f, 0.15f, 0),_duration).SetLoops(-1, LoopType.Yoyo);  
        }

        private void OnTriggerEnter2D(Collider2D col)
        {
            if (col.gameObject.GetComponent<PlayerController>())
            {
                IsCollected = true;
            }
        }

        private void Update()
        {
            if (IsCollected)
            {
                var step = _gameConfig.ExpDict[_expType].FollowSpeed * Time.deltaTime; 
                transform.position = Vector3.MoveTowards(transform.position, _playerModel.PlayerPosition, step);
                if (transform.position == _playerModel.PlayerPosition)
                {
                    _pool.Release(this);
                    _expModel.AddExp(_gameConfig.ExpDict[_expType].Value);
                }
            }
        }
    }
}
