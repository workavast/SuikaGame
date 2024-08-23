using System;
using SuikaGame.Scripts.Saves.Analytics;
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
        public readonly AnalyticsSettings AnalyticsSettings = new();

        private readonly ISettings[] _settings;

#if !UNITY_EDITOR && UNITY_WEBGL
        private static readonly IPlayerDataSaveAndLoader SaveAndLoader = new GamePushSaveAndLoader();
#else
        private static readonly IPlayerDataSaveAndLoader SaveAndLoader = new DesktopSaveAndLoader();
#endif
        
        public event Action OnInit;
        public event Action OnSave;

        private PlayerData()
        {
            _settings = new ISettings[]
            {
                LocalizationSettings,
                VolumeSettings, 
                ScoreSettings,
                TutorialSettings,
                GameplaySceneSettings,
                SkinsSettings,
                CoinsSettings,
                AnalyticsSettings
            };
            
            SaveAndLoader.OnLoaded += LoadData;
        }
        
        public void InvokeLoad() 
            => SaveAndLoader.Load();

        private void LoadData(PlayerDataSave save)
        {
            LocalizationSettings.LoadData(save.localizationSettingsSave);
            VolumeSettings.LoadData(save.volumeSettingsSave);
            ScoreSettings.LoadData(save.scoreSettingsSave);
            TutorialSettings.LoadData(save.tutorialSettingsSave);
            GameplaySceneSettings.LoadData(save.gameplaySceneSettingsSave);
            SkinsSettings.LoadData(save.skinsSettingsSave);
            CoinsSettings.LoadData(save.coinsSettingsSave);
            AnalyticsSettings.LoadData(save.analyticsSettingsSave);
            
            if(!_isLoaded)
                SubsAfterFirstLoad();
            
            _isLoaded = true;
        }
        
        public void SaveData()
        {
            foreach (var setting in _settings)
                if (setting.IsChanged)
                {
                    SaveAndLoader.Save(this);
                    foreach (var setting2 in _settings) 
                        setting2.ResetChangedMarker();
                    OnSave?.Invoke();
                    return;
                }
        }

        private void SubsAfterFirstLoad()
        {
            Debug.Log("-||- SubsAfterFirstLoad");
            OnInit?.Invoke();
        }
        
#if UNITY_EDITOR
        [MenuItem("SuikaGame/Reset saves")]
#endif
        public static void ResetSave()
        {
            SaveAndLoader.ResetSave();
            SaveAndLoader.Load();
            Debug.Log($"Saves reseted");
        }
    }
}
