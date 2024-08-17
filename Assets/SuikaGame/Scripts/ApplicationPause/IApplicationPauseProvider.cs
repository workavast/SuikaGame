using System;

namespace SuikaGame.Scripts.ApplicationPause
{
    public interface IApplicationPauseProvider
    {
        public bool IsPause { get; }

        public event Action<bool> OnApplicationPauseChanged;
    }
}