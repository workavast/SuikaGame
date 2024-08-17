using SuikaGame.Scripts.Ad.AdTimer;
using SuikaGame.Scripts.Ad.FullScreenAd;
using SuikaGame.Scripts.Ad.RewardedAd;
using Zenject;

namespace SuikaGame.Scripts.Ad
{
    public class AdInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            BindAdTimer();
            BindFullScreenAd();
            BindRewardedAd();
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
        
        private void BindRewardedAd()
        {
            Container.BindInterfacesTo<GamePushRewardedAd>().FromNew().AsSingle().NonLazy();
        }

        private void BindAdController()
        {
            Container.BindInterfacesTo<GamePushAdController>().FromNew().AsSingle().NonLazy();
        }
    }
}