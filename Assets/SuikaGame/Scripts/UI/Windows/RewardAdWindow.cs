using Avastrad.UI.UI_System;
using SuikaGame.Scripts.Ad.RewardedAd;
using SuikaGame.Scripts.Coins;
using SuikaGame.Scripts.UI.Elements.Buttons;
using UnityEngine;
using Zenject;

namespace SuikaGame.Scripts.UI.Windows
{
    public class RewardAdWindow : UI_ScreenBase
    {
        [SerializeField] private SomeButton adButton;
        
        private IRewardedAd _rewardedAd;
        private CoinsConfig _coinsConfig;

        [Inject]
        public void Construct(IRewardedAd rewardedAd, CoinsConfig coinsConfig)
        {
            _rewardedAd = rewardedAd;
            _coinsConfig = coinsConfig;
            
            _rewardedAd.OnAdClose += Close;
        }

        public override void Initialize()
        {
            Hide();
            
            adButton.SetText($"+{_coinsConfig.CoinsPerRewardedAd}");
            adButton.OnClick += _ShowRewardAd;
        }
        
        public void _ShowRewardAd() 
            => _rewardedAd.Show(RewardIds.AdditionalCoins);
        
        private void Close() 
            => UI_Controller.ToggleScreen(ScreenType.RewardedAd, false);
    }
}