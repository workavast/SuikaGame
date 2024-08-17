using System;
using GamePush;
using SuikaGame.Scripts.Ad;
using SuikaGame.Scripts.ApplicationFocus;
using SuikaGame.Scripts.ApplicationPause;
using UnityEngine;

namespace SuikaGame.Scripts.GamePausing
{
    public class GamePushPauser : IGamePauser, IDisposable
    {
        private readonly IApplicationFocusProvider _applicationFocusProvider;
        private readonly IApplicationPauseProvider _applicationPauseProvider;
        private readonly IAdProvider _adProvider;

        public GamePushPauser(IApplicationFocusProvider applicationFocusProvider, 
            IApplicationPauseProvider applicationPauseProvider, IAdProvider adProvider)
        {
            _applicationFocusProvider = applicationFocusProvider;
            _applicationPauseProvider = applicationPauseProvider;
            _adProvider = adProvider;

            _applicationFocusProvider.OnApplicationFocusChanged += OnApplicationFocusChanged;
            _applicationPauseProvider.OnApplicationPauseChanged += OnApplicationPauseChanged;

            _adProvider.OnAdStart += GlobalGamePause.Pause;
            _adProvider.OnAdClose += GlobalGamePause.Continue;
            
            _adProvider.OnAdStart += LocalGamePause.Pause;
            _adProvider.OnAdClose += LocalGamePause.Continue;
            
            GP_Game.OnPause += GlobalGamePause.Pause;
            GP_Game.OnResume += GlobalGamePause.Continue;
            
            GP_Game.OnPause += LocalGamePause.Pause;
            GP_Game.OnResume += LocalGamePause.Continue;
        }

        
        private void OnApplicationFocusChanged(bool isFocus)
        {
            Debug.LogWarning($"OnApplicationFocusChanged {isFocus}");
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

        private void OnApplicationPauseChanged(bool isPause)
        {
            Debug.LogWarning($"OnApplicationPauseChanged {isPause}");
            if (isPause)
            {
                GlobalGamePause.Pause();
                LocalGamePause.Pause();
            }
            else
            {
                GlobalGamePause.Continue();
                LocalGamePause.Continue();
            }
        }
        
        public void Dispose()
        {
            _applicationFocusProvider.OnApplicationFocusChanged -= OnApplicationFocusChanged;
            _applicationPauseProvider.OnApplicationPauseChanged -= OnApplicationPauseChanged;

            _adProvider.OnAdStart -= GlobalGamePause.Pause;
            _adProvider.OnAdClose -= GlobalGamePause.Continue;
            
            _adProvider.OnAdStart -= LocalGamePause.Pause;
            _adProvider.OnAdClose -= LocalGamePause.Continue;
            
            GP_Game.OnPause -= GlobalGamePause.Pause;
            GP_Game.OnResume -= GlobalGamePause.Continue;
            
            GP_Game.OnPause -= LocalGamePause.Pause;
            GP_Game.OnResume -= LocalGamePause.Continue;
        }
    }
}