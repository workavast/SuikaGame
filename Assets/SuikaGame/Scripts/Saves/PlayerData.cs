using System;
using SuikaGame.Scripts.Saves.Audio;
using SuikaGame.Scripts.Saves.Coins;
using SuikaGame.Scripts.Saves.GameplayScene;
using SuikaGame.Scripts.Saves.Localization;
using SuikaGame.Scripts.Saves.Score;
using SuikaGame.Scripts.Saves.SkinsPacks;
using SuikaGame.Scripts.Saves.Train;
using UnityEditor;
using UnityEngine;

namespace SuikaGame.Scripts.Saves
{
    public class PlayerData
    {
        private static bool _isLoaded;
        private static PlayerData _instance;
        public static PlayerData Instance => _instance ??= new PlayerData();

        public readonly LocalizationSettings LocalizationSettings = new();
        public readonly VolumeSettings VolumeSettings = new();
        public readonly ScoreSettings ScoreSettings = new();
        public readonly TutorialSettings TutorialSettings = new();
        public readonly GameplaySceneSettings GameplaySceneSettings = new();
        public readonly SkinsSettings SkinsSettings = new();
        public readonly CoinsSettings CoinsSettings = new();

#if !UNITY_EDITOR && UNITY_WEBGL
        private static readonly IPlayerDataSaveAndLoader SaveAndLoader = new GamePushSaveAndLoader();
#else
        private static readonly IPlayerDataSaveAndLoader SaveAndLoader = new DesktopSaveAndLoader();
#endif
        
        public event Action OnInit;

        public void ResetSaves() 
            => SaveAndLoader.ResetSave();

        public void InvokeLoad()
        {
            SaveAndLoader.OnLoaded += LoadData;
            SaveAndLoader.Load();
        }
        
        private void LoadData(PlayerDataSave save)
        {
            SaveAndLoader.OnLoaded -= LoadData;
            
            LocalizationSettings.LoadData(save.localizationSettingsSave);
            VolumeSettings.LoadData(save.volumeSettingsSave);
            ScoreSettings.LoadData(save.scoreSettingsSave);
            TutorialSettings.LoadData(save.tutorialSettingsSave);
            GameplaySceneSettings.LoadData(save.gameplaySceneSettingsSave);
            SkinsSettings.LoadData(save.skinsSettingsSave);
            CoinsSettings.LoadData(save.coinsSettingsSave);
            
            if(!_isLoaded)
                SubsAfterFirstLoad();
            _isLoaded = true;
        }
        
        private void SaveData() 
            => SaveAndLoader.Save(this);

        private void SubsAfterFirstLoad()
        {
            Debug.Log("-||- SubsAfterFirstLoad");
            
            ISettings[] settings =
            {
                LocalizationSettings,
                VolumeSettings, 
                ScoreSettings,
                TutorialSettings,
                GameplaySceneSettings,
                SkinsSettings,
                CoinsSettings
            };
            foreach (var setting in settings)
                setting.OnChange += SaveData;
            
            OnInit?.Invoke();
        }
        
#if UNITY_EDITOR
        [MenuItem("SuikaGame/Reset saves")]
#endif
        public static void ResetSave()
            => SaveAndLoader.ResetSave();
    }
}
