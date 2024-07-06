using System.Threading.Tasks;
using GamePush;
using SuikaGame.Scripts.Saves;
using UnityEngine;
using UnityEngine.Localization.Settings;

namespace SuikaGame.Scripts.Localization.Changer
{
    public class GamePushLocalizationChanger : ILocalizationChanger
    {
        public static int LocalizationIndex => PlayerData.Instance.LocalizationSettings.LocalizationId;
        
        private bool _active;
        
        public async void ChangeLocalization(int localizationId)
        {
            if(_active || PlayerData.Instance.LocalizationSettings.LocalizationId == localizationId)
                return;
            
            if (localizationId >= LocalizationSettings.AvailableLocales.Locales.Count || localizationId < 0)
            {
                Debug.LogError("Invalid localization Id");
                return;
            }
            
            await ApplyLocalization(localizationId);
            
            PlayerData.Instance.LocalizationSettings.ChangeLocalization(localizationId);
        }

        private async Task ApplyLocalization(int localizationId)
        {
            _active = true;

            var handleTask = LocalizationSettings.InitializationOperation;
            await handleTask.Task;
            LocalizationSettings.SelectedLocale = LocalizationSettings.AvailableLocales.Locales[localizationId];
            GP_Language.Change(GetLanguage(localizationId));
            
            _active = false;
        }

        private Language GetLanguage(int localizationId)
        {
            switch (localizationId)
            {
                case 1:
                    return Language.Russian;
                case 0:
                    return Language.English;
                default:
                    return Language.English;
            }
        }
    }
}