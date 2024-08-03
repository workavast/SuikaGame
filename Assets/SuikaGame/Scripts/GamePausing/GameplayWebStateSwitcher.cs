using GamePush;

namespace SuikaGame.Scripts.GamePausing
{
    public class GameplayWebStateSwitcher
    {
        public GameplayWebStateSwitcher()
        {
            LocalGamePause.OnContinued += GameplayStartMessage;
            LocalGamePause.OnPaused += GameplayStopMessage;
        }

        private static void GameplayStartMessage() 
            => GP_Game.GameplayStart();

        private static void GameplayStopMessage() 
            => GP_Game.GameplayStop();
    }
}