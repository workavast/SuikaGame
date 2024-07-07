using System;
using UnityEngine;

namespace SuikaGame.Scripts.Game
{
    public static class GamePauser
    {
        private static int _pauseRequestsCount = 0;
        
        public static event Action OnGamePaused; 
        public static event Action OnGameContinued;
        
        public static void Pause()
        {
            Debug.LogWarning("--||-- Pause");

            Time.timeScale = 0;
            _pauseRequestsCount++;
            if (_pauseRequestsCount == 1)
                OnGamePaused?.Invoke();
        }

        public static void Continue()
        {
            Debug.LogWarning("--||-- Continue");
            
            if (_pauseRequestsCount <= 0)
            {
                Debug.LogWarning("Superfluous call of game continue");
                return;
            }
            
            _pauseRequestsCount--;
            if (_pauseRequestsCount <= 0)
            {
                Time.timeScale = 1;
                OnGameContinued?.Invoke();
            }
        }
    }
}