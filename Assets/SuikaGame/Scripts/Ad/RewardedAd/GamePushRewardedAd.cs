using System;
using GamePush;

namespace SuikaGame.Scripts.Ad.RewardedAd
{
    public class GamePushRewardedAd : IRewardedAd
    {
        public event Action OnAdStart;
        public event Action OnAdClose;
        public event Action<string> OnAdReward; 

        public GamePushRewardedAd()
        {
            GP_Ads.OnRewardedStart += () => OnAdStart?.Invoke();
            GP_Ads.OnRewardedClose += (_) => OnAdClose?.Invoke();
            GP_Ads.OnRewardedReward += (rewardId) => OnAdReward?.Invoke(rewardId);
        }
        
        public void Show(string rewardId)
        {
            GP_Ads.ShowRewarded(rewardId);
        }
    }
}