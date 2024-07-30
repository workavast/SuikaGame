using Avastrad.UI.UI_System;
using SuikaGame.Scripts.Ad.RewardedAd;
using SuikaGame.Scripts.Coins;
using SuikaGame.Scripts.Game;
using SuikaGame.Scripts.UI.AnimationBlocks;
using SuikaGame.Scripts.UI.AnimationBlocks.Blocks;
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
        private AnimationBlocksHolder _animationBlocksHolder;

        [Inject]
        public void Construct(IRewardedAd rewardedAd, CoinsConfig coinsConfig)
        {
            _rewardedAd = rewardedAd;
            _coinsConfig = coinsConfig;
            
            _rewardedAd.OnAdClose += Close;
        }

        public override void Initialize()
        {
            _animationBlocksHolder = new AnimationBlocksHolder(new IAnimationBlock[]
                { animationFadeBlock, animationScaleBlock });
            
            adButton.SetText($"+{_coinsConfig.CoinsPerRewardedAd}");
            adButton.OnClick += ShowRewardAd;

            HideInstantly(false);
        }
        
        public override void Show()
        {
            GamePauser.Pause();
            gameObject.SetActive(true);
            _animationBlocksHolder.Show();
        }

        public override void Hide()
        {
            _animationBlocksHolder.Hide(() =>
            {
                gameObject.SetActive(false);
                GamePauser.Continue();
            });
        }

        public override void HideInstantly() 
            => HideInstantly(true);

        private void HideInstantly(bool withGameContinue)
        {
            _animationBlocksHolder.HideInstantly();
            gameObject.SetActive(false);
            if (withGameContinue) 
                GamePauser.Continue();
        }

        private void ShowRewardAd() 
            => _rewardedAd.Show(RewardIds.AdditionalCoins);
        
        private void Close() 
            => UI_Controller.ToggleScreen(ScreenType.RewardedAd, false);
    }
}