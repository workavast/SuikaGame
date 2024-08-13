﻿using System;
using GamePush;
using SuikaGame.Scripts.Saves.GameplayScene;
using SuikaGame.Scripts.Saves.Localization;
using SuikaGame.Scripts.Saves.SkinsPacks;
using UnityEngine;

namespace SuikaGame.Scripts.Saves
{
    public class GamePushSaveAndLoader : IPlayerDataSaveAndLoader
    {
        public event Action<PlayerDataSave> OnLoaded;

        public void Load()
        {
            var save = new PlayerDataSave
            {
                volumeSettingsSave =
                {
                    MusicVolume = GP_Player.GetFloat(GamePushSaveParametersNames.MusicVolume),
                    EffectsVolume = GP_Player.GetFloat(GamePushSaveParametersNames.EffectsVolume)
                },
                scoreSettingsSave =
                {
                    ScoreRecord = (int)GP_Player.GetScore()
                },
                localizationSettingsSave =
                    JsonUtility.FromJson<LocalizationSettingsSave>(
                        GP_Player.GetString(GamePushSaveParametersNames.LocalizationSave)),
                gameplaySceneSettingsSave =
                    JsonUtility.FromJson<GameplaySceneSettingsSave>(
                        GP_Player.GetString(GamePushSaveParametersNames.GameplaySave)),
                skinsSettingsSave =
                    JsonUtility.FromJson<SkinsSettingsSave>(
                        GP_Player.GetString(GamePushSaveParametersNames.SkinsSave)),
                coinsSettingsSave =
                {
                    Coins = GP_Player.GetInt(GamePushSaveParametersNames.CoinsSave)
                }
            };

            OnLoaded?.Invoke(save);
        }

        public void Save(PlayerData playerData)
        {
            GP_Player.Set(GamePushSaveParametersNames.MusicVolume, playerData.VolumeSettings.MusicVolume);
            GP_Player.Set(GamePushSaveParametersNames.EffectsVolume, playerData.VolumeSettings.EffectsVolume);
            GP_Player.Set(GamePushSaveParametersNames.LocalizationSave, JsonUtility.ToJson(new LocalizationSettingsSave(playerData.LocalizationSettings)));
            GP_Player.Set(GamePushSaveParametersNames.GameplaySave, JsonUtility.ToJson(new GameplaySceneSettingsSave(playerData.GameplaySceneSettings)));
            GP_Player.Set(GamePushSaveParametersNames.SkinsSave, JsonUtility.ToJson(new SkinsSettingsSave(playerData.SkinsSettings)));
            GP_Player.Set(GamePushSaveParametersNames.CoinsSave, playerData.CoinsSettings.Coins);
            
            GP_Player.SetScore(playerData.ScoreSettings.ScoreRecord);
            
            GP_Player.Sync();
        }

        public void ResetSave()
            => GP_Player.ResetPlayer();
    }
}