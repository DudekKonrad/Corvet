using Application.Contexts.GameplayContext.Models;
using UnityEngine;
using Zenject;

namespace Application.Contexts.GameplayContext
{
    public class GameplayContextInstaller : MonoInstaller
    {
        [SerializeField] private PlayerController _playerController;
        [SerializeField] private Transform _enemiesContainer;
        public override void InstallBindings()
        {
            Container.BindInterfacesAndSelfTo<PlayerInputModel>().AsSingle().NonLazy();
            Container.BindInterfacesAndSelfTo<PlayerModel>().AsSingle().NonLazy();
            Container.BindInterfacesAndSelfTo<EnemyModel>().AsTransient().NonLazy();
            Container.BindInstance(_playerController).WithId(nameof(_playerController)).AsCached().NonLazy();
            Container.BindInstance(_enemiesContainer).WithId(nameof(_enemiesContainer)).AsCached().NonLazy();
            Container.Bind<ScoreModel>().AsSingle().NonLazy();
        }
    }
}
