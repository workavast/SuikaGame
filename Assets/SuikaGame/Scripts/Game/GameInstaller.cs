using Zenject;

namespace SuikaGame.Scripts.Game
{
    public class GameInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            Container.BindInterfacesTo<GamePushGame>().FromNew().AsSingle().NonLazy();
        }
    }
}