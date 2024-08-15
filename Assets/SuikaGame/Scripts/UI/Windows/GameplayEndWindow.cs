using Avastrad.UI.UI_System;
using SuikaGame.Scripts.GamePausing;
using SuikaGame.Scripts.Leaderboard;
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
        private ILeaderBoardPositionLoader _leaderBoardPositionLoader;
        private AnimationBlocksHolder _animationBlocksHolder;

        [Inject]
        public void Construct(IScoreCounter scoreCounter, ILeaderBoardPositionLoader leaderBoardPositionLoader)
        {
            _scoreCounter = scoreCounter;
            _leaderBoardPositionLoader = leaderBoardPositionLoader;
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
            gotCoinsView.SetValue();
            TryShowRecordTitle();
            _animationBlocksHolder.Show();
            GameplayWebStateSwitcher.GameplayStopMessage();
        }

        public override void Hide()
        {
            _animationBlocksHolder.Hide(() =>
            {
                gameObject.SetActive(false);
                GameplayWebStateSwitcher.GameplayStartMessage();
            });
        }

        public override void HideInstantly()
        {
            _animationBlocksHolder.HideInstantly();
            gameObject.SetActive(false);
            GameplayWebStateSwitcher.GameplayStartMessage();
        }

        private void TryShowRecordTitle()
        {
            if (_scoreCounter.IsNewRecord)
                newRecordTitle.Show();
            else
                newRecordTitle.Hide();
        }
    }
}