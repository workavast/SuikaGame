using Zenject;

namespace SuikaGame.Scripts.Coins
{
    public class CoinsCounterInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            Container.BindInterfacesTo<CoinsCounter>().FromNew().AsSingle();
        }
    }
}