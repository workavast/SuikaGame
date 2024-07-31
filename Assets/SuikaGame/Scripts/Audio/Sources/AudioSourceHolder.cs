using System;
using Avastrad.CustomTimer;
using Avastrad.PoolSystem;
using UnityEngine;

namespace SuikaGame.Scripts.Audio.Sources
{
    [RequireComponent(typeof(AudioSource))]
    public class AudioSourceHolder : MonoBehaviour, IPoolable<AudioSourceHolder, AudioSourceType>
    {
        [field: SerializeField] public AudioSourceType PoolId { get; private set; }
        
        private AudioSource _audioSource;
        private Timer _existTimer;
        
        public event Action<AudioSourceHolder> DestroyElementEvent;
        public event Action<AudioSourceHolder> ReturnElementEvent;

        private void Awake()
        {
            _audioSource = GetComponent<AudioSource>();
            _existTimer = new Timer(_audioSource.clip.length + 0.1f);
            _existTimer.OnTimerEnd += () => ReturnElementEvent?.Invoke(this);
        }

        private void Update() 
            => _existTimer.Tick(Time.deltaTime);

        public void SetPitch(float newPitch) 
            => _audioSource.pitch = newPitch;

        public void OnElementExtractFromPool()
        {
#if UNITY_EDITOR
            gameObject.SetActive(true);
#endif
            _existTimer.Reset();
            _audioSource.Play();
        }

        public void OnElementReturnInPool()
        {
            _existTimer.SetPause();
            _audioSource.Stop();
#if UNITY_EDITOR
            gameObject.SetActive(false);
#endif
        }
    }
}