using System;
using UnityEngine;

namespace SuikaGame.Scripts.GamePausing
{
    public static class GlobalGamePause
    {
        private static int _pauseRequestsCount = 0;
        
        public static bool IsPaused { get; private set; }
        
        public static event Action OnPaused; 
        public static event Action OnContinued;
        
        public static void Pause()
        {
            IsPaused = true;
            _pauseRequestsCount++;
            if (_pauseRequestsCount == 1)
                OnPaused?.Invoke();
        }

        public static void Continue()
        {
            if (_pauseRequestsCount <= 0)
            {
                Debug.LogError("Superfluous call of global game continue");
                return;
            }

            _pauseRequestsCount--;
            if (_pauseRequestsCount <= 0)
            {
                IsPaused = false;
                OnContinued?.Invoke();
            }
        }
    }
}