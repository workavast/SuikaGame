using Avastrad.UI.UI_System;
using SuikaGame.Scripts.Game;

namespace SuikaGame.Scripts.UI.Windows
{
    public class GameplaySettingsWindow : UI_ScreenBase
    {
        public override void Initialize()
            => Hide();

        public override void Show()
        {
            GamePauser.Pause();
            gameObject.SetActive(true);
        }

        public override void Hide()
        {
            GamePauser.Continue();
            gameObject.SetActive(false);
        }
    }
}