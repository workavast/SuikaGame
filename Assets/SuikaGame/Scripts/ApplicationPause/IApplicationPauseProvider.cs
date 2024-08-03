using System;

namespace SuikaGame.Scripts.ApplicationPause
{
    public interface IApplicationPauseProvider
    {
        public event Action<bool> OnApplicationPauseChanged;
    }
}