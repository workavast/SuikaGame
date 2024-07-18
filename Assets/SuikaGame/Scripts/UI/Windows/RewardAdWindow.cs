using Avastrad.UI.UI_System;
using SuikaGame.Scripts.Ad.RewardedAd;
using Zenject;

namespace SuikaGame.Scripts.UI.Windows
{
    public class RewardAdWindow : UI_ScreenBase
    {
        private IRewardedAd _rewardedAd;

        [Inject]
        public void Construct(IRewardedAd rewardedAd)
        {
            _rewardedAd = rewardedAd;
            _rewardedAd.OnAdClose += Close;
        }

        public override void Initialize() 
            => Hide();

        public void _ShowRewardAd() 
            => _rewardedAd.Show(RewardIds.AdditionalCoins);

        private void Close() 
            => UI_Controller.ToggleScreen(ScreenType.RewardedAd, false);
    }
}