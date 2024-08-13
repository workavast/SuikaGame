using GamePush;
using UnityEngine;

namespace SuikaGame.Scripts.GamePausing
{
    public class GameplayWebStateSwitcher
    {
        private static int _stopsCounter;
        
        public GameplayWebStateSwitcher()
        {
            LocalGamePause.OnContinued += GameplayStartMessage;
            LocalGamePause.OnPaused += GameplayStopMessage;
        }

        public static void GameplayStartMessage()
        {
            if (_stopsCounter <= 0)
            {
                Debug.LogWarning($"Superfluous GameplayStartMessage call");
                return;
            }
            
            _stopsCounter--;
            if (_stopsCounter <= 0) 
                GP_Game.GameplayStart();
        }

        public static void GameplayStopMessage()
        {
            _stopsCounter++;
            if (_stopsCounter == 1) 
                GP_Game.GameplayStop();
        }
    }
}