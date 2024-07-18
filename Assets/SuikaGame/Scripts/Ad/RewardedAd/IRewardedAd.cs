using System;

namespace SuikaGame.Scripts.Ad.RewardedAd
{
    public interface IRewardedAd
    {
        public event Action OnAdStart;
        public event Action OnAdClose;
        public event Action<string> OnAdReward;

        public void Show(string rewardId);
    }
}