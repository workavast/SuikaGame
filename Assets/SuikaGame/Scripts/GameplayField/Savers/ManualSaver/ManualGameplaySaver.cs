using System;
using Avastrad.CustomTimer;
using SuikaGame.Scripts.Saves;
using UnityEngine;
using Zenject;

namespace SuikaGame.Scripts.GameplayField.Savers.ManualSaver
{
    public class ManualGameplaySaver : IManualGameplaySaver, ITickable, IDisposable
    {
        private readonly IGameplaySaver _saver;
        private readonly Timer _resetTimer;
        
        public bool SaveAllowed { get; private set; }
        public IReadOnlyTimer ReadOnlyTimer => _resetTimer;

        public ManualGameplaySaver(ManualGameplaySaverConfig manualGameplaySaverConfig, IGameplaySaver saver)
        {
            _saver = saver;
            
            _resetTimer = new Timer(manualGameplaySaverConfig.SavePause, manualGameplaySaverConfig.SavePause);
            _resetTimer.OnTimerEnd += AllowManualSave;

            if (_resetTimer.TimerIsEnd) 
                AllowManualSave();
        }
        
        public void Tick() 
            => _resetTimer.Tick(Time.unscaledDeltaTime);

        public void Save()
        {
            if (!SaveAllowed)
            {
                Debug.LogWarning("You cant save at the moment");
                return;
            }
            
            _saver.Apply();
            SaveAllowed = false;
            _resetTimer.Reset();
            PlayerData.Instance.SaveData();
        }
        
        private void AllowManualSave()
        {
            SaveAllowed = true;
        }

        public void Dispose()
        {
            _resetTimer.OnTimerEnd -= AllowManualSave;
        }
    }
}