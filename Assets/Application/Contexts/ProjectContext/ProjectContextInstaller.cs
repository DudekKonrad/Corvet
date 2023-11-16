using Application.Contexts.ProjectContext.Configs;
using UnityEngine;
using Zenject;

namespace Application.Contexts.ProjectContext
{
    public class ProjectContextInstaller : MonoInstaller
    {
        [SerializeField] private CorvetGameConfig _gameConfig;

        public override void InstallBindings()
        {
            Container.BindInstance(_gameConfig);
        }
    }
}