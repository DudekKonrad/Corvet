using Application.Contexts.ProjectContext.Configs;
using Application.Contexts.ProjectContext.Signals;
using UnityEngine;
using Zenject;

namespace Application.Contexts.ProjectContext
{
    public class ProjectContextInstaller : MonoInstaller<ProjectContextInstaller>
    {
        [SerializeField] private CorvetGameConfig _gameConfig;

        public override void InstallBindings()
        {
            SignalBusInstaller.Install(Container);
            Container.BindInstance(_gameConfig);
            Container.DeclareSignal<CorvetProjectSignals.ExpChangedSignal>();
        }
    }
}