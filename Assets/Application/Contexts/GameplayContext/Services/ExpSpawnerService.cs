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
        private readonly List<ExpPointMediator> _expList = new List<ExpPointMediator>();

        public ObjectPool<ExpPointMediator> ExpPool { get; private set; }

        private List<ExpPointMediator> ExpList => _expList;

        [Inject]
        private void Construct()
        {
            ExpPool = new ObjectPool<ExpPointMediator>(OnCreateExp, OnGetExp, OnReleaseExp,
                defaultCapacity: 100);
        }

        private ExpPointMediator OnCreateExp()
        {
            var exp = _diContainer.InstantiatePrefabForComponent<ExpPointMediator>(
                _gameConfig.ExpDict[ExpType.Medium].Prefab, _expContainer);
            exp.Init(ExpPool);
            ExpList.Add(exp);
            return exp;
        }

        private void OnReleaseExp(ExpPointMediator expPointMediator)
        {
            expPointMediator.IsActiveInPool = false;
            expPointMediator.IsCollected = false;
            expPointMediator.gameObject.SetActive(false);
        }

        private void OnGetExp(ExpPointMediator expPoint)
        {
            expPoint.transform.localPosition = Vector3.zero;
            expPoint.IsActiveInPool = true;
            expPoint.gameObject.SetActive(true);
        }
    }
}