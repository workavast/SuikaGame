using SuikaGame.Scripts.Ad.RewardedAd;

namespace SuikaGame.Scripts.Coins
{
    public class CoinsRewarding
    {
        private readonly CoinsConfig _coinsConfig;
        private readonly IRewardedAd _rewardedAd;
        private readonly ICoinsChangeModel _coinsChangeModel;

        public CoinsRewarding(CoinsConfig coinsConfig, IRewardedAd rewardedAd, ICoinsChangeModel coinsChangeModel)
        {
            _coinsConfig = coinsConfig;
            _rewardedAd = rewardedAd;
            _coinsChangeModel = coinsChangeModel;

            _rewardedAd.OnAdReward += CheckReward;
        }

        private void CheckReward(string rewardId)
        {
            if (rewardId == RewardIds.AdditionalCoins)
                _coinsChangeModel.ChangeCoinsValue(_coinsConfig.CoinsPerRewardedAd);
        }
    }
}