using System;
using Avastrad.CustomTimer;
using Avastrad.PoolSystem;
using SuikaGame.Scripts.GamePausing;
using UnityEngine;

namespace SuikaGame.Scripts.Audio.Sources
{
    [RequireComponent(typeof(AudioSource))]
    public class AudioSourceHolder : MonoBehaviour, IPoolable<AudioSourceHolder, AudioSourceType>
    {
        [field: SerializeField] public AudioSourceType PoolId { get; private set; }
        [SerializeField] private bool useLocalPause = true;
        
        private AudioSource _audioSource;
        private Timer _existTimer;
        
        public event Action<AudioSourceHolder> DestroyElementEvent;
        public event Action<AudioSourceHolder> ReturnElementEvent;

        private void Awake()
        {
            _audioSource = GetComponent<AudioSource>();
            
            _existTimer = new Timer(_audioSource.clip.length);
            _existTimer.OnTimerEnd += () => ReturnElementEvent?.Invoke(this);
        }

        private void Update() 
            => _existTimer.Tick(useLocalPause ? Time.deltaTime : Time.unscaledDeltaTime);

        public void SetPitch(float newPitch) 
            => _audioSource.pitch = newPitch;

        public void OnElementExtractFromPool()
        {
#if UNITY_EDITOR
            gameObject.SetActive(true);
#endif

            if (useLocalPause)
            {
                LocalGamePause.OnPaused += Pause;
                LocalGamePause.OnContinued += Continue;
            }
            else
            {
                GlobalGamePause.OnPaused += Pause;
                GlobalGamePause.OnContinued += Continue;
            }
            
            _existTimer.Reset();
            _audioSource.Play();
        }

        public void OnElementReturnInPool()
        {
            _existTimer.SetPause();
            _audioSource.Stop();
            
            LocalGamePause.OnPaused -= Pause;
            LocalGamePause.OnContinued -= Continue;
            GlobalGamePause.OnPaused -= Pause;
            GlobalGamePause.OnContinued -= Continue;
            
#if UNITY_EDITOR
            gameObject.SetActive(false);
#endif
        }

        private void Pause() 
            => _audioSource.Pause();

        private void Continue() 
            => _audioSource.Play();

        private void OnDestroy() 
            => DestroyElementEvent?.Invoke(this);
    }
}