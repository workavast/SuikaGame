﻿using System;
using SuikaGame.Scripts.Saves.Audio;
using SuikaGame.Scripts.Saves.Localization;
using SuikaGame.Scripts.Saves.Score;
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

        public PlayerDataSave()
        {
            localizationSettingsSave = new();
            volumeSettingsSave = new();
            tutorialSettingsSave = new();
            scoreSettingsSave = new();
        }
        
        public PlayerDataSave(PlayerData playerData)
        {
            localizationSettingsSave = new LocalizationSettingsSave(playerData.LocalizationSettings);
            volumeSettingsSave = new VolumeSettingsSave(playerData.VolumeSettings);
            scoreSettingsSave = new ScoreSettingsSave(playerData.ScoreSettings);
            tutorialSettingsSave = new TutorialSettingsSave(playerData.TutorialSettings);
        }
    }
}