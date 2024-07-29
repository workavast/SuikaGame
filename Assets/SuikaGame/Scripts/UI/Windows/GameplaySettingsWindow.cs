using Avastrad.UI.UI_System;
using SuikaGame.Scripts.Game;
using SuikaGame.Scripts.UI.Elements.AnimationBlocks;
using UnityEngine;

namespace SuikaGame.Scripts.UI.Windows
{
    public class GameplaySettingsWindow : UI_ScreenBase
    {
        [SerializeField] private AnimationFadeBlock animationFadeBlock;
        [SerializeField] private AnimationMoveBlock animationMoveBlock;
        
        public override void Initialize() 
            => Hide();

        public override void Show()
        {
            GamePauser.Pause();
            gameObject.SetActive(true);
            animationFadeBlock.Show();
            animationMoveBlock.Show();
        }

        public override void Hide()
        {
            animationFadeBlock.Hide();
            animationMoveBlock.Hide(() =>
            {
                gameObject.SetActive(false);
                GamePauser.Continue();
            });
        }
    }
}