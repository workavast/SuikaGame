using System;

namespace SuikaGame.Scripts.ApplicationFocus
{
    public interface IApplicationFocusProvider
    {
        public bool IsFocus { get; }

        public event Action<bool> OnApplicationFocusChanged;
    }
}