using System;
using UnityEngine;

namespace SuikaGame.Scripts.Game
{
    public static class GamePauser
    {
        public static event Action OnGamePaused; 
        public static event Action OnGameContinued; 
        
        public static void Pause()
        {
            Time.timeScale = 0;
            OnGamePaused?.Invoke();
        }

        public static void Continue()
        {
            Time.timeScale = 1;
            OnGameContinued?.Invoke();
        }
    }
}