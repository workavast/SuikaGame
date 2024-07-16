using System;
using SuikaGame.Scripts.Saves.Audio;
using SuikaGame.Scripts.Saves.Coins;
using SuikaGame.Scripts.Saves.GameplayScene;
using SuikaGame.Scripts.Saves.Localization;
using SuikaGame.Scripts.Saves.Score;
using SuikaGame.Scripts.Saves.SkinsPacks;
using SuikaGame.Scripts.Saves.Train;

namespace SuikaGame.Scripts.Saves
{
    [Serializable]
    public class PlayerDataSave
    {
        public LocalizationSettingsSave localizationSettingsSave;
        public VolumeSettingsSave volumeSettingsSave;
        public ScoreSettingsSave scoreSettingsSave;
        public TutorialSettingsSave tutorialSettingsSave;
        public GameplaySceneSettingsSave gameplaySceneSettingsSave;
        public SkinsSettingsSave skinsSettingsSave;
        public CoinsSettingsSave coinsSettingsSave;

        public PlayerDataSave()
        {
            localizationSettingsSave = new();
            volumeSettingsSave = new();
            tutorialSettingsSave = new();
            scoreSettingsSave = new();
            gameplaySceneSettingsSave = new();
            skinsSettingsSave = new();
            coinsSettingsSave = new();
        }
        
        public PlayerDataSave(PlayerData playerData)
        {
            localizationSettingsSave = new LocalizationSettingsSave(playerData.LocalizationSettings);
            volumeSettingsSave = new VolumeSettingsSave(playerData.VolumeSettings);
            scoreSettingsSave = new ScoreSettingsSave(playerData.ScoreSettings);
            tutorialSettingsSave = new TutorialSettingsSave(playerData.TutorialSettings);
            gameplaySceneSettingsSave = new GameplaySceneSettingsSave(playerData.GameplaySceneSettings);
            skinsSettingsSave = new SkinsSettingsSave(playerData.SkinsSettings);
            coinsSettingsSave = new CoinsSettingsSave(playerData.CoinsSettings);
        }
    }
}