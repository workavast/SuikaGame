using System;

namespace SuikaGame.Scripts.Saves.Audio
{
    [Serializable]
    public sealed class VolumeSettingsSave
    {
        public float MasterVolume = 1f;
        public float MusicVolume = 1f;
        public float EffectsVolume = 1f;

        public VolumeSettingsSave()
        {
            MasterVolume = 1;
            MusicVolume = 1f;
            EffectsVolume = 1f;
        }
        
        public VolumeSettingsSave(VolumeSettings settings)
        {
            MasterVolume = settings.Master;
            MusicVolume = settings.MusicVolume;
            EffectsVolume = settings.EffectsVolume;
        }
    }
}