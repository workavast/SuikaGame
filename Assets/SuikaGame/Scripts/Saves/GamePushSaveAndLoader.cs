using System;
using GamePush;
using SuikaGame.Scripts.Saves.GameplayScene;
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
                localizationSettingsSave =
                {
                    LocalizationId = GP_Player.GetInt(GamePushSaveParametersNames.LocalizationId)
                },
                scoreSettingsSave =
                {
                    ScoreRecord = (int)GP_Player.GetScore()
                },
                gameplaySceneSettingsSave = JsonUtility.FromJson<GameplaySceneSettingsSave>(GP_Player.GetString(GamePushSaveParametersNames.GameplaySave))
            };

            OnLoaded?.Invoke(save);
        }

        public void Save(PlayerData playerData)
        {
            GP_Player.Set(GamePushSaveParametersNames.MusicVolume, playerData.VolumeSettings.MusicVolume);
            GP_Player.Set(GamePushSaveParametersNames.EffectsVolume, playerData.VolumeSettings.EffectsVolume);
            GP_Player.Set(GamePushSaveParametersNames.LocalizationId, playerData.LocalizationSettings.LocalizationId);
            GP_Player.Set(GamePushSaveParametersNames.GameplaySave, JsonUtility.ToJson(new GameplaySceneSettingsSave(playerData.GameplaySceneSettings)));
            
            GP_Player.SetScore(playerData.ScoreSettings.ScoreRecord);
            
            GP_Player.Sync();
        }

        public void ResetSave()
            => throw new NotImplementedException();
    }
}