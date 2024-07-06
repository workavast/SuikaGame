using System;
using System.Threading.Tasks;
using SuikaGame.Scripts.Localization.Determinant;
using UnityEngine;
using UnityEngine.Localization.Settings;

namespace SuikaGame.Scripts.Localization
{
    public class LocalizationInitializer
    {
#if UNITY_EDITOR || !UNITY_WEBGL
        private readonly ILocalizationDeterminant _localizationDeterminant = new SystemLocalizationDeterminant();
#else
        private readonly ILocalizationDeterminant _localizationDeterminant = new GamePushLocalizationDeterminant();
#endif
        
        public async void InitLocalizationSettings(
            SuikaGame.Scripts.Saves.Localization.LocalizationSettings localizationSettings, Action onComplete)
        {
            await Init(localizationSettings);
            onComplete?.Invoke();
        }

        private async Task Init(SuikaGame.Scripts.Saves.Localization.LocalizationSettings localizationSettings)
        {
            var handleTask = LocalizationSettings.InitializationOperation;
            await handleTask.Task;
            
            int langIndex = 1;
            if (!localizationSettings.Initializaed)
                langIndex = _localizationDeterminant.GetLocalization();
            else
                langIndex = localizationSettings.LocalizationId;
            
            LocalizationSettings.SelectedLocale = LocalizationSettings.AvailableLocales.Locales[langIndex];
            
            var handleTask2 = LocalizationSettings.InitializationOperation;
            await handleTask2.Task;
            localizationSettings.ChangeLocalization(langIndex);
            localizationSettings.Initialize();
            
            Debug.Log("-||- LocalizationInitializer");
        }
    }
}
