using System;
using SuikaGame.Scripts.ApplicationFocus;

namespace SuikaGame.Scripts.GameplayField.Savers
{
    public class ApplicationFocusSaver : IDisposable
    {
        private readonly IApplicationFocusProvider _applicationFocusProvider;
        private readonly IGameplaySaver _gameplaySaver;

        public ApplicationFocusSaver(IApplicationFocusProvider applicationFocusProvider, IGameplaySaver gameplaySaver)
        {
            _applicationFocusProvider = applicationFocusProvider;
            _gameplaySaver = gameplaySaver;

            _applicationFocusProvider.OnApplicationFocusChanged += OnApplicationFocusChanged;
        }
        
        private void OnApplicationFocusChanged(bool hasFocus)
        {
            if (!hasFocus) 
                _gameplaySaver?.Save();
        }

        public void Dispose()
        {
            _applicationFocusProvider.OnApplicationFocusChanged -= OnApplicationFocusChanged;
        }
    }
}