using System;

namespace SuikaGame.Scripts.Saves.Audio
{
    [Serializable]
    public sealed class VolumeSettingsSave
    {
        public float MasterVolume = 1f;
        public float MusicVolume = 0.8f;
        public float EffectsVolume = 0.8f;

        public VolumeSettingsSave()
        {
            MasterVolume = 1;
            MusicVolume = 0.8f;
            EffectsVolume = 0.8f;
        }
        
        public VolumeSettingsSave(VolumeSettings settings)
        {
            MasterVolume = settings.Master;
            MusicVolume = settings.MusicVolume;
            EffectsVolume = settings.EffectsVolume;
        }
    }
}