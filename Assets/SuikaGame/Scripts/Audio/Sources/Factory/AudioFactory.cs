using System;
using Avastrad.PoolSystem;
using UnityEngine;
using Zenject;
using Random = UnityEngine.Random;

namespace SuikaGame.Scripts.Audio.Sources.Factory
{
    public class AudioFactory : MonoBehaviour, IAudioFactory
    {
        private AudioSourcesConfig _audioSourcesConfig;
        private Pool<AudioSourceHolder, AudioSourceType> _pool;
        
        public event Action<AudioSourceHolder> OnCreate;

        [Inject]
        public void Construct(AudioSourcesConfig audioSourcesConfig)
        {
            _audioSourcesConfig = audioSourcesConfig;
        }
        
        private void Awake()
        {
            _pool = new Pool<AudioSourceHolder, AudioSourceType>(InstantiateEntity);
        }
        
        public void Create(AudioSourceType audioSourceType, Vector2 position)
        {
            _pool.ExtractElement(audioSourceType, out var someAudioSource);
            
            someAudioSource.transform.position = position;

            var pitch = _audioSourcesConfig.Data[audioSourceType].Pitch;
            var pitchRange = _audioSourcesConfig.Data[audioSourceType].PitchRange;
            pitch += Random.Range(-pitchRange, pitchRange);
            
            someAudioSource.SetPitch(pitch);
            
            OnCreate?.Invoke(someAudioSource);
        }

        private AudioSourceHolder InstantiateEntity(AudioSourceType audioSourceType)
        {
            if (!_audioSourcesConfig.Data.ContainsKey(audioSourceType))
                throw new ArgumentOutOfRangeException($"_audioConfig doesnt contains {audioSourceType}");

            var someAudioSource = Instantiate(_audioSourcesConfig.Data[audioSourceType].AudioSourceHolderPrefab, transform);
            return someAudioSource;
        }
    }
}