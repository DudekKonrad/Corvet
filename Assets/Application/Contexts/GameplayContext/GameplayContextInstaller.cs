using Application.Contexts.GameplayContext.Controllers;
using Application.Contexts.GameplayContext.Models;
using Application.Contexts.GameplayContext.Services;
using UnityEngine;
using Zenject;

namespace Application.Contexts.GameplayContext
{
    public class GameplayContextInstaller : MonoInstaller
    {
        [SerializeField] private PlayerController _playerController;
        [SerializeField] private EnemySpawnerService _enemySpawnerService;
        [SerializeField] private Transform _enemiesContainer;
        [SerializeField] private Transform _expContainer;

        public override void InstallBindings()
        {
            Container.BindInterfacesAndSelfTo<PlayerInputModel>().AsSingle().NonLazy();
            Container.BindInterfacesAndSelfTo<ExpSpawnerService>().AsSingle().NonLazy();
            Container.BindInterfacesAndSelfTo<ProjectilesService>().AsSingle().NonLazy();
            Container.BindInterfacesAndSelfTo<PlayerModel>().AsSingle().NonLazy();
            Container.BindInterfacesAndSelfTo<EnemyModel>().AsTransient().NonLazy();
            Container.BindInstance(_playerController).WithId(nameof(_playerController)).AsCached().NonLazy();
            Container.BindInstance(_enemiesContainer).WithId(nameof(_enemiesContainer)).AsCached().NonLazy();
            Container.BindInstance(_expContainer).WithId(nameof(_expContainer)).AsCached().NonLazy();
            Container.BindInstance(_enemySpawnerService).WithId(nameof(_enemySpawnerService)).AsCached().NonLazy();
            Container.Bind<ExpModel>().AsSingle().NonLazy();
        }
    }
}