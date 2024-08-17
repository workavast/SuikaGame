using System;
using Avastrad.CustomTimer;
using SuikaGame.Scripts.ApplicationPause;
using UnityEngine;
using Zenject;

namespace SuikaGame.Scripts.GameplayField.Savers.AutoSaver
{
    public class AutoGameplaySaver : ITickable, IDisposable
    {
        private readonly IGameplaySaver _saver;
        private readonly Timer _timer;

        public AutoGameplaySaver(AutoGameplaySaverConfig autoGameplaySaverConfig, IGameplaySaver saver, 
            IApplicationPauseProvider applicationPauseProvider)
        {
            _saver = saver;
            _timer = new Timer(autoGameplaySaverConfig.SavePause);

            _saver.OnSave += ResetTimer;
            _timer.OnTimerEnd += InvokeSave;
        }
        
        public void Tick() 
            => _timer.Tick(Time.deltaTime);
        
        private void ResetTimer() 
            => _timer.Reset();
        
        private void InvokeSave()
        {
            _saver.Save();
            _timer.Reset();
        }
        
        public void Dispose()
        {
            _saver.OnSave -= ResetTimer; 
            _timer.OnTimerEnd -= InvokeSave;
        }
    }
}