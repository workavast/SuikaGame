using System;
using GamePush;
using UnityEngine;

namespace SuikaGame.Scripts.GamePausing
{
    public static class LocalGamePause
    {
        private static int _pauseRequestsCount = 0;
        
        public static bool IsPaused { get; private set; }
        
        public static event Action OnPaused; 
        public static event Action OnContinued;
        
        public static void Pause()
        {
            IsPaused = true;
            Time.timeScale = 0;
            _pauseRequestsCount++;
            if (_pauseRequestsCount == 1)
                OnPaused?.Invoke();
        }

        public static void Continue()
        {
            if (_pauseRequestsCount <= 0)
            {
                Debug.LogWarning("Superfluous call of game continue");
                return;
            }

            _pauseRequestsCount--;
            if (_pauseRequestsCount <= 0)
            {
                IsPaused = false;
                Time.timeScale = 1;
                OnContinued?.Invoke();
            }
        }
    }
}