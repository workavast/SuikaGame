using Avastrad.UI.UI_System;
using SuikaGame.Scripts.Ad.RewardedAd;
using SuikaGame.Scripts.Coins;
using SuikaGame.Scripts.UI.Elements.AnimationBlocks;
using SuikaGame.Scripts.UI.Elements.Buttons;
using UnityEngine;
using Zenject;

namespace SuikaGame.Scripts.UI.Windows
{
    public class RewardAdWindow : UI_ScreenBase
    {
        [SerializeField] private SomeButton adButton;
        [Space]
        [SerializeField] private AnimationFadeBlock animationFadeBlock;
        [SerializeField] private AnimationScaleBlock animationScaleBlock;
        
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
            adButton.OnClick += ShowRewardAd;
        }
        
        public override void Show()
        {
            gameObject.SetActive(true);
            animationFadeBlock.Show();
            animationScaleBlock.Show();
        }

        public override void Hide()
        {
            animationFadeBlock.Hide();
            animationScaleBlock.Hide(() => gameObject.SetActive(false));
        }

        private void ShowRewardAd() 
            => _rewardedAd.Show(RewardIds.AdditionalCoins);
        
        private void Close() 
            => UI_Controller.ToggleScreen(ScreenType.RewardedAd, false);
    }
}