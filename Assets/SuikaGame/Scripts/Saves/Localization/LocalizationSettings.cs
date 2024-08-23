using System;
using UnityEngine;

namespace SuikaGame.Scripts.Saves.Localization
{
    public class LocalizationSettings : ISettings
    {
        public bool IsChanged { get; private set; }

        public bool Initializaed { get; private set; } = false;
        public int LocalizationId { get; private set; }

        public LocalizationSettings()
        {
            Initializaed = false;
            LocalizationId = 1;
        }

        public void Initialize()
        {
            if(Initializaed)
                return;

            IsChanged = true;
            Initializaed = true;
            PlayerData.Instance.SaveData();
        }

        public void ChangeLocalization(int newLocalizationId)
        {
            if (newLocalizationId == LocalizationId)
                return;
            
            IsChanged = true;
            LocalizationId = newLocalizationId;
        }

        public void LoadData(LocalizationSettingsSave settingsSave)
        {
            Initializaed = settingsSave.Initializaed;
            LocalizationId = settingsSave.LocalizationId;
        }
        
        public void ResetChangedMarker() 
            => IsChanged = false;
    }
}