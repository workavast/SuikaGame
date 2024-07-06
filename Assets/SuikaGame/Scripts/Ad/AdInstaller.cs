using SuikaGame.Scripts.Ad.AdTimer;
using SuikaGame.Scripts.Ad.FullScreenAd;
using Zenject;

namespace SuikaGame.Scripts.Ad
{
    public class AdInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            BindAdTimer();
            BindFullScreenAd();
            BindAdController();
        }

        private void BindAdTimer()
        {
            Container.BindInterfacesTo<GamePushAdTimer>().FromNew().AsSingle().NonLazy();
        }
        
        private void BindFullScreenAd()
        {
            Container.BindInterfacesTo<GamePushFullScreenAd>().FromNew().AsSingle().NonLazy();
        }

        private void BindAdController()
        {
            Container.BindInterfacesAndSelfTo<GamePushAdController>().FromNew().AsSingle().NonLazy();
        }
    }
}