using SuikaGame.Scripts.GamePausing;
using UnityEngine;

namespace SuikaGame.Scripts.Audio.Sources
{
    [RequireComponent(typeof(AudioSource))]
    public class AmbienceSource : MonoBehaviour
    {
        private AudioSource _audioSource;
        
        private void Awake()
        {
            _audioSource = GetComponent<AudioSource>();
            
            GlobalGamePause.OnPaused += Pause;
            GlobalGamePause.OnContinued += Continue;
        }

        private void Pause() 
            => _audioSource.Pause();

        private void Continue() 
            => _audioSource.Play();

        private void OnDestroy()
        {
            GlobalGamePause.OnPaused -= Pause;
            GlobalGamePause.OnContinued -= Continue;
        }
    }
}