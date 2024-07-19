using System;
using UnityEngine;

namespace SuikaGame.Scripts.Saves.Localization
{
    public class LocalizationSettings : ISettings
    {
        public bool Initializaed { get; private set; } = false;
        public int LocalizationId { get; private set; }
        
        public event Action OnChange;

        public LocalizationSettings()
        {
            Initializaed = false;
            LocalizationId = 1;
        }

        public void Initialize()
        {
            if(Initializaed)
                return;
            
            Initializaed = true;
            OnChange?.Invoke();
        }

        public void ChangeLocalization(int newLocalizationId)
        {
            if (newLocalizationId == LocalizationId)
                return;
            
            LocalizationId = newLocalizationId;
            OnChange?.Invoke();
        }

        public void LoadData(LocalizationSettingsSave settingsSave)
        {
            Initializaed = settingsSave.Initializaed;
            LocalizationId = settingsSave.LocalizationId;
        }
    }
}