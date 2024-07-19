using Zenject;

namespace SuikaGame.Scripts.GameplayControlling
{
    public class GameplayControllerInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            Container.BindInterfacesTo<GameplayController>().FromNew().AsSingle().NonLazy();
        }
    }
}