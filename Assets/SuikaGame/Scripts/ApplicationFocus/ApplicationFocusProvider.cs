using System;
using UnityEngine;

namespace SuikaGame.Scripts.ApplicationFocus
{
    public class ApplicationFocusProvider : MonoBehaviour, IApplicationFocusProvider
    {
        public bool IsFocus { get; private set; }

        public event Action<bool> OnApplicationFocusChanged;

#if !UNITY_EDITOR
        private void OnApplicationFocus(bool hasFocus)
        {
            if (IsFocus == hasFocus)
                return;

            IsFocus = hasFocus;
            OnApplicationFocusChanged?.Invoke(hasFocus);
        }
#endif
    }
}