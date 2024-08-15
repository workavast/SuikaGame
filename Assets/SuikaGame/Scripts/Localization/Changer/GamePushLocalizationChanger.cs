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
                case 0:
                    return Language.English;
                case 1:
                    return Language.French;
                case 2:
                    return Language.German;
                case 3:
                    return Language.Italian;
                case 4:
                    return Language.Portuguese;
                case 5:
                    return Language.Russian;
                case 6:
                    return Language.Spanish;
                case 7:
                    return Language.Turkish;
                default:
                    return Language.English;
            }
        }
    }
}