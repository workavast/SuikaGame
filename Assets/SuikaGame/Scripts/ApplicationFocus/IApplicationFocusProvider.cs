using System;

namespace SuikaGame.Scripts.ApplicationFocus
{
    public interface IApplicationFocusProvider
    {
        public event Action<bool> OnApplicationFocusChanged;
    }
}