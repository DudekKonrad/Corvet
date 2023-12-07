using Application.Contexts.ProjectContext.Configs;
using Application.Contexts.ProjectContext.Fsm;
using UnityEngine;
using Zenject;

namespace Application.Contexts.ProjectContext
{
    public class ProjectContextInstaller : MonoInstaller<ProjectContextInstaller>
    {
        [SerializeField] private CorvetGameConfig _gameConfig;
        [SerializeField] private SoundConfig _soundConfig;
        [SerializeField] private GameObject _loadingScreen;

        public override void InstallBindings()
        {
            SignalBusInstaller.Install(Container);
            Container.BindInstance(_gameConfig);
            Container.BindInstance(_soundConfig);
            Container.InstantiatePrefab(_loadingScreen);
            Container.BindInterfacesAndSelfTo<GameFsm>().AsSingle().NonLazy();
            Container.BindInterfacesAndSelfTo<SoundService>().AsSingle().NonLazy();
        }
    }
}