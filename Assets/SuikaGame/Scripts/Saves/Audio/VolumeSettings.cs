using System;
using UnityEngine;

namespace SuikaGame.Scripts.Saves.Audio
{
    public sealed class VolumeSettings : ISettings
    {
        public bool IsChanged { get; private set; }
        public float Master { get; private set; }
        public float MusicVolume { get; private set; }
        public float EffectsVolume { get; private set; }

        private float _prevMasterVolume;
        private float _prevMusicVolume;
        private float _prevEffectsVolume;
        
        public VolumeSettings()
        {
            Master = 1;
            MusicVolume = 1f;
            EffectsVolume = 1f;
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
                IsChanged = true;
                
                _prevMasterVolume = Master;
                _prevMusicVolume = MusicVolume;
                _prevEffectsVolume = EffectsVolume;
            }
        }
        
        public void LoadData(VolumeSettingsSave volumeSettingsSave)
        {
            Master = volumeSettingsSave.MasterVolume;
            MusicVolume = volumeSettingsSave.MusicVolume;
            EffectsVolume = volumeSettingsSave.EffectsVolume;
        }
        
        public void ResetChangedMarker() 
            => IsChanged = false;
    }
}