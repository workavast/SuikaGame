using Avastrad.EventBusFramework;
using Zenject;

namespace SuikaGame.Scripts
{
    public class EventBusInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            Container.BindInterfacesTo<EventBus>().FromNew().AsSingle().NonLazy();
        }
    }
}