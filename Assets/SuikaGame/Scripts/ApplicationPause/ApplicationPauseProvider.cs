using System;
using UnityEngine;

namespace SuikaGame.Scripts.ApplicationPause
{
    public class ApplicationPauseProvider : MonoBehaviour, IApplicationPauseProvider
    {
        public event Action<bool> OnApplicationPauseChanged;

#if !UNITY_EDITOR
        private void OnApplicationPause(bool pauseStatus) 
            => OnApplicationPauseChanged?.Invoke(pauseStatus);
#endif
    }
}