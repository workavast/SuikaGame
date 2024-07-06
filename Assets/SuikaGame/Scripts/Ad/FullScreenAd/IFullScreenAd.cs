using System;

namespace SuikaGame.Scripts.Ad.FullScreenAd
{
    public interface IFullScreenAd
    {
        public event Action OnAdStart;
        public event Action OnAdClose;

        public void TryShow();
    }
}