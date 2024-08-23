using System;
using Avastrad.CustomTimer;
using SuikaGame.Scripts.Saves;
using UnityEngine;
using Zenject;

namespace SuikaGame.Scripts.GameplayField.Savers.AutoSaver
{
    public class AutoGameplaySaver : ITickable, IDisposable
    {
        private readonly Timer _timer;

        public AutoGameplaySaver(AutoGameplaySaverConfig autoGameplaySaverConfig)
        {
            _timer = new Timer(autoGameplaySaverConfig.SavePause);

            PlayerData.Instance.OnSave += ResetTimer;
            _timer.OnTimerEnd += InvokeSave;
        }
        
        public void Tick() 
            => _timer.Tick(Time.deltaTime);
        
        private void ResetTimer() 
            => _timer.Reset();
        
        private void InvokeSave()
        {
            PlayerData.Instance.SaveData();
            _timer.Reset();
        }
        
        public void Dispose()
        {
            PlayerData.Instance.OnSave -= ResetTimer;
            _timer.OnTimerEnd -= InvokeSave;
        }
    }
}