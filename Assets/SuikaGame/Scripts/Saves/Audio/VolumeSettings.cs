using System;
using UnityEngine;

namespace SuikaGame.Scripts.Saves.Audio
{
    public sealed class VolumeSettings : ISettings
    {
        public float Master { get; private set; }
        public float MusicVolume { get; private set; }
        public float EffectsVolume { get; private set; }

        private float _prevMasterVolume;
        private float _prevMusicVolume;
        private float _prevEffectsVolume;
        
        public event Action OnChange;

        public VolumeSettings()
        {
            Master = 1;
            MusicVolume = 0.8f;
            EffectsVolume = 0.8f;
        }
    
        public void ChangeMasterVolume(float newVolume) 
            => Master = newVolume;

        public void ChangeMusicVolume(float newVolume) 
            => MusicVolume = newVolume;

        public void ChangeEffectsVolume(float newVolume) 
            => EffectsVolume = newVolume;

        public void Apply()
        {
            const float tolerance = 0.001f;
            if (Math.Abs(_prevMasterVolume - Master) > tolerance ||
                Math.Abs(_prevMusicVolume - MusicVolume) > tolerance ||
                Math.Abs(_prevEffectsVolume - EffectsVolume) > tolerance)
            {
                Debug.Log("Apply");
                _prevMasterVolume = Master;
                _prevMusicVolume = MusicVolume;
                _prevEffectsVolume = EffectsVolume;

                OnChange?.Invoke();
            }
        }
        
        public void LoadData(VolumeSettingsSave volumeSettingsSave)
        {
            Master = volumeSettingsSave.MasterVolume;
            MusicVolume = volumeSettingsSave.MusicVolume;
            EffectsVolume = volumeSettingsSave.EffectsVolume;
        }
    }
}