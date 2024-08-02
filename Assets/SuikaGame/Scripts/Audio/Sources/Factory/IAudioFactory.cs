using UnityEngine;

namespace SuikaGame.Scripts.Audio.Sources.Factory
{
    public interface IAudioFactory
    {
        public void Create(AudioSourceType audioSourceType, Vector2 position = new());
    }
}