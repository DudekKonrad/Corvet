using System.Collections.Generic;
using Application.Contexts.GameplayContext.Mediators;
using Application.Contexts.ProjectContext.Configs;
using UnityEngine;
using UnityEngine.Pool;
using Zenject;

namespace Application.Contexts.GameplayContext.Services
{
    public class ExpSpawnerService
    {
        [Inject] private readonly DiContainer _diContainer;
        [Inject(Id = nameof(_expContainer))] private readonly Transform _expContainer;
        [Inject] private readonly CorvetGameConfig _gameConfig;

        public ObjectPool<ExpMediator> ExpPool { get; private set; }

        public List<ExpMediator> ExpList { get; } = new();

        [Inject]
        private void Construct()
        {
            ExpPool = new ObjectPool<ExpMediator>(OnCreateExp, OnGetExp, OnReleaseExp,
                defaultCapacity: 100);
        }

        private ExpMediator OnCreateExp()
        {
            var exp = _diContainer.InstantiatePrefabForComponent<ExpMediator>(
                _gameConfig.ExpDict[ExpType.Medium].Prefab, _expContainer);
            exp.Init(ExpPool);
            ExpList.Add(exp);
            return exp;
        }

        private void OnReleaseExp(ExpMediator expMediator)
        {
            expMediator.IsActiveInPool = false;
            expMediator.gameObject.SetActive(false);
        }

        private void OnGetExp(ExpMediator exp)
        {
            exp.transform.localPosition = Vector3.zero;
            exp.IsActiveInPool = true;
            exp.gameObject.SetActive(true);
        }
    }
}