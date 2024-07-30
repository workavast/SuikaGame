using Avastrad.UI.UI_System;
using SuikaGame.Scripts.Score;
using SuikaGame.Scripts.UI.AnimationBlocks;
using SuikaGame.Scripts.UI.AnimationBlocks.Blocks;
using SuikaGame.Scripts.UI.Elements;
using SuikaGame.Scripts.UI.Elements.Views;
using UnityEngine;
using Zenject;

namespace SuikaGame.Scripts.UI.Windows
{
    public class GameplayEndWindow : UI_ScreenBase
    {
        [SerializeField] private AnimationFadeBlock backgroundFadeBlock;
        [SerializeField] private AnimationScaleBlock animationScaleBlock;
        [Space]        
        [SerializeField] private NewRecordTitle newRecordTitle;
        [SerializeField] private GetedCoinsView gotCoinsView;

        private IScoreCounter _scoreCounter;
        private AnimationBlocksHolder _animationBlocksHolder;

        [Inject]
        public void Construct(IScoreCounter scoreCounter)
        {
            _scoreCounter = scoreCounter;
        }

        public override void Initialize()
        {
            _animationBlocksHolder = new AnimationBlocksHolder(new IAnimationBlock[]
                { backgroundFadeBlock, animationScaleBlock });
           
            HideInstantly();
        }

        public override void Show()
        {
            gameObject.SetActive(true);
            gotCoinsView.SetValue(_scoreCounter.Score);
            TryShowRecordTitle();
            _animationBlocksHolder.Show();
        }

        public override void Hide()
        {
            _animationBlocksHolder.Hide(() => gameObject.SetActive(false));
        }

        public override void HideInstantly()
        {
            _animationBlocksHolder.HideInstantly();
            gameObject.SetActive(false);
        }

        private void TryShowRecordTitle()
        {
            if (_scoreCounter.Record <= _scoreCounter.Score)
                newRecordTitle.Show();
            else
                newRecordTitle.Hide();
        }
    }
}