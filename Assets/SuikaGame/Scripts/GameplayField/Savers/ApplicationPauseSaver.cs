using System;
using SuikaGame.Scripts.ApplicationPause;
using SuikaGame.Scripts.Saves;

namespace SuikaGame.Scripts.GameplayField.Savers
{
    public class ApplicationPauseSaver : IDisposable
    {
        private readonly IApplicationPauseProvider _applicationFocusProvider;
        private readonly IGameplaySaver _gameplaySaver;

        public ApplicationPauseSaver(IApplicationPauseProvider applicationFocusProvider, IGameplaySaver gameplaySaver)
        {
            _applicationFocusProvider = applicationFocusProvider;
            _gameplaySaver = gameplaySaver;

            _applicationFocusProvider.OnApplicationPauseChanged += OnApplicationFocusChanged;
        }
        
        private void OnApplicationFocusChanged(bool isPause)
        {
            if (isPause)
            {
                _gameplaySaver?.Apply();
                PlayerData.Instance.SaveData();
            }
        }

        public void Dispose()
        {
            _applicationFocusProvider.OnApplicationPauseChanged -= OnApplicationFocusChanged;
        }
    }
}