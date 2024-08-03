using System;
using GamePush;
using SuikaGame.Scripts.ApplicationFocus;
using SuikaGame.Scripts.ApplicationPause;

namespace SuikaGame.Scripts.GamePausing
{
    public class GamePushPauser : IGamePauser, IDisposable
    {
        private readonly IApplicationFocusProvider _applicationFocusProvider;
        private readonly IApplicationPauseProvider _applicationPauseProvider;

        public GamePushPauser(IApplicationFocusProvider applicationFocusProvider, 
            IApplicationPauseProvider applicationPauseProvider)
        {
            _applicationFocusProvider = applicationFocusProvider;
            _applicationPauseProvider = applicationPauseProvider;

            _applicationFocusProvider.OnApplicationFocusChanged += OnApplicationFocusChanged;
            _applicationPauseProvider.OnApplicationPauseChanged += OnApplicationPauseChanged;
            
            GP_Game.OnPause += GlobalGamePause.Pause;
            GP_Game.OnResume += GlobalGamePause.Continue;
            
            GP_Game.OnPause += LocalGamePause.Pause;
            GP_Game.OnResume += LocalGamePause.Continue;
        }

        private static void OnApplicationFocusChanged(bool isFocus)
        {
            if (isFocus)
            {
                GlobalGamePause.Continue();
                LocalGamePause.Continue();
            }
            else
            {
                GlobalGamePause.Pause();
                LocalGamePause.Pause();
            }
        }

        private static void OnApplicationPauseChanged(bool isPause)
        {
            if (isPause)
            {
                GlobalGamePause.Continue();
                LocalGamePause.Continue();
            }
            else
            {
                GlobalGamePause.Pause();
                LocalGamePause.Pause();
            }
        }
        
        public void Dispose()
        {
            _applicationFocusProvider.OnApplicationFocusChanged -= OnApplicationFocusChanged;

            GP_Game.OnPause -= GlobalGamePause.Pause;
            GP_Game.OnResume -= GlobalGamePause.Continue;
            
            GP_Game.OnPause -= LocalGamePause.Pause;
            GP_Game.OnResume -= LocalGamePause.Continue;
        }
    }
}