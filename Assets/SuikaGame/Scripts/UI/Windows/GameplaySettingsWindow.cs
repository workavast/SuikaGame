using Avastrad.UI.UI_System;
using SuikaGame.Scripts.Game;
using SuikaGame.Scripts.UI.AnimationBlocks;
using SuikaGame.Scripts.UI.AnimationBlocks.Blocks;
using UnityEngine;

namespace SuikaGame.Scripts.UI.Windows
{
    public class GameplaySettingsWindow : UI_ScreenBase
    {
        [SerializeField] private AnimationFadeBlock animationFadeBlock;
        [SerializeField] private AnimationMoveBlock animationMoveBlock;

        private AnimationBlocksHolder _animationBlocksHolder;
        
        public override void Initialize()
        {
            _animationBlocksHolder = new AnimationBlocksHolder(new IAnimationBlock[]
                { animationFadeBlock, animationMoveBlock });
            
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
    }
}