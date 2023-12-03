using Application.Contexts.ProjectContext.Configs;
using Application.Contexts.ProjectContext.Signals;
using UnityEngine;
using Zenject;

namespace Application.Contexts.ProjectContext
{
    public class ProjectContextInstaller : MonoInstaller<ProjectContextInstaller>
    {
        [SerializeField] private CorvetGameConfig _gameConfig;
        [SerializeField] private SoundConfig _soundConfig;

        public override void InstallBindings()
        {
            SignalBusInstaller.Install(Container);
            Container.BindInstance(_gameConfig);
            Container.BindInstance(_soundConfig);
            Container.BindInterfacesAndSelfTo<SoundService>().AsSingle().NonLazy();
            Container.DeclareSignal<CorvetProjectSignals.ExpChangedSignal>();
            Container.DeclareSignal<CorvetProjectSignals.LevelUpSignal>();
            Container.DeclareSignal<CorvetProjectSignals.PlaySoundSignal>();
        }
    }
}