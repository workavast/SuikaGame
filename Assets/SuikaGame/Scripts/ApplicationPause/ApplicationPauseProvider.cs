using System;
using UnityEngine;

namespace SuikaGame.Scripts.ApplicationPause
{
    public class ApplicationPauseProvider : MonoBehaviour, IApplicationPauseProvider
    {
        public bool IsPause { get; private set; }

        public event Action<bool> OnApplicationPauseChanged;

#if !UNITY_EDITOR
        private void OnApplicationPause(bool pauseStatus)
        {
            if (IsPause == pauseStatus)
                return;
            
            IsPause = pauseStatus;
            OnApplicationPauseChanged?.Invoke(pauseStatus);
        }
#endif
    }
}