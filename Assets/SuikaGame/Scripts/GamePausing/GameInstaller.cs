using Zenject;

namespace SuikaGame.Scripts.GamePausing
{
    public class GameInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            Container.BindInterfacesTo<GamePushPauser>().FromNew().AsSingle().NonLazy();
        }
    }
}