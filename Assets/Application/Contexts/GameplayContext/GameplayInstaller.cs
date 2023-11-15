using Application.Contexts.GameplayContext.Models;
using Zenject;

namespace Application.Contexts.GameplayContext
{
    public class GameplayInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            Container.BindInterfacesAndSelfTo<PlayerInputModel>().AsSingle().NonLazy();
        }
    }
}
