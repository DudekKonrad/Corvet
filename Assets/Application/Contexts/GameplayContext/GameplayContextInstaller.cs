using Application.Contexts.GameplayContext.Models;
using Zenject;

namespace Application.Contexts.GameplayContext
{
    public class GameplayContextInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            Container.BindInterfacesAndSelfTo<PlayerInputModel>().AsSingle().NonLazy();
            Container.BindInterfacesAndSelfTo<PlayerModel>().AsSingle().NonLazy();
            Container.Bind<ScoreModel>().AsSingle().NonLazy();
        }
    }
}
