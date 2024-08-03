using System;
using UnityEngine;

namespace SuikaGame.Scripts.GamePausing
{
    public static class GlobalGamePause
    {
        public static bool IsPaused { get; private set; }
        
        public static event Action OnPaused; 
        public static event Action OnContinued;
        
        public static void Pause()
        {
            if (IsPaused)
                return;

            IsPaused = true;
            OnPaused?.Invoke();
        }

        public static void Continue()
        {
            if (!IsPaused)
                return;
            
            IsPaused = false;
            OnContinued?.Invoke();
        }
    }
}