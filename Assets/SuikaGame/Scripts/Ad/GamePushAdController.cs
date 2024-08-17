using System;
using SuikaGame.Scripts.Ad.AdTimer;
using SuikaGame.Scripts.Ad.FullScreenAd;
using SuikaGame.Scripts.Ad.RewardedAd;
using Zenject;

namespace SuikaGame.Scripts.Ad
{
    public class GamePushAdController : IAdProvider, IDisposable
    {
        private IAdTimer _adTimer;
        private IFullScreenAd _fullScreenAd;
        private IRewardedAd _rewardedAd;
        
        public event Action OnAdStart;
        public event Action OnAdClose;
        
        [Inject]
        public void Construct(IAdTimer adTimer, IFullScreenAd fullScreenAd, IRewardedAd rewardedAd)
        {
            _adTimer = adTimer;
            _fullScreenAd = fullScreenAd;
            _rewardedAd = rewardedAd;

            _fullScreenAd.OnAdStart += () => OnAdStart?.Invoke();
            _rewardedAd.OnAdStart += () => OnAdStart?.Invoke();
            
            _fullScreenAd.OnAdClose += () => OnAdClose?.Invoke();
            _rewardedAd.OnAdClose += () => OnAdClose?.Invoke();
            
            _adTimer.OnAdPrepared += _fullScreenAd.TryShow;
        }
        
        public void Dispose()
            => _adTimer.OnAdPrepared -= _fullScreenAd.TryShow;
    }
}