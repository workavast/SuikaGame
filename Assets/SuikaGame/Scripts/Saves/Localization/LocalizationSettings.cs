using System;

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
            Initializaed = true;
            OnChange?.Invoke();
        }

        public void ChangeLocalization(int newLocalizationId)
        {
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