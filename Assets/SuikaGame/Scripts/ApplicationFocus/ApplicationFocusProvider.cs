using System;
using UnityEngine;

namespace SuikaGame.Scripts.ApplicationFocus
{
    public class ApplicationFocusProvider : MonoBehaviour, IApplicationFocusProvider
    {
        public event Action<bool> OnApplicationFocusChanged;

        private void OnApplicationFocus(bool hasFocus) 
            => OnApplicationFocusChanged?.Invoke(hasFocus);
    }
}