using System;
using GamePush;

namespace SuikaGame.Scripts.Ad.FullScreenAd
{
    public class GamePushFullScreenAd : IFullScreenAd
    {
        public event Action OnAdStart;
        public event Action OnAdClose;

        public GamePushFullScreenAd()
        {
            GP_Ads.OnFullscreenStart += () => OnAdStart?.Invoke();
            GP_Ads.OnFullscreenClose += (_) => OnAdClose?.Invoke();
        }
        
        public void TryShow()
        {
            GP_Ads.ShowFullscreen();
        }
    }
}