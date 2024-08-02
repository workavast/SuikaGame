using System;

namespace SuikaGame.Scripts.Saves.Audio
{
    [Serializable]
    public sealed class VolumeSettingsSave
    {
        public float MasterVolume = 1f;
        public float MusicVolume = 0.75f;
        public float EffectsVolume = 0.75f;

        public VolumeSettingsSave()
        {
            MasterVolume = 1;
            MusicVolume = 0.75f;
            EffectsVolume = 0.75f;
        }
        
        public VolumeSettingsSave(VolumeSettings settings)
        {
            MasterVolume = settings.Master;
            MusicVolume = settings.MusicVolume;
            EffectsVolume = settings.EffectsVolume;
        }
    }
}