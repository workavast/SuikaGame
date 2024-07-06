using System;
using SuikaGame.Scripts.Ad.AdTimer;
using SuikaGame.Scripts.Ad.FullScreenAd;
using Zenject;

namespace SuikaGame.Scripts.Ad
{
    public class GamePushAdController : IDisposable
    {
        private IAdTimer _adTimer;
        private IFullScreenAd _fullScreenAd;
        
        [Inject]
        public void Construct(IAdTimer adTimer, IFullScreenAd fullScreenAd)
        {
            _adTimer = adTimer;
            _fullScreenAd = fullScreenAd;
            
            _adTimer.OnAdPrepared += _fullScreenAd.TryShow;
        }
        
        public void Dispose()
            => _adTimer.OnAdPrepared -= _fullScreenAd.TryShow;
    }
}