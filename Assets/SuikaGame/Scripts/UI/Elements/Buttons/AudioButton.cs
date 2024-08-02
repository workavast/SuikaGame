using SuikaGame.Scripts.Audio.Sources;
using SuikaGame.Scripts.Audio.Sources.Factory;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace SuikaGame.Scripts.UI.Elements.Buttons
{
    [RequireComponent(typeof(AudioButton))]
    public class AudioButton : MonoBehaviour
    {
        private IAudioFactory _audioFactory;
        
        [Inject]
        public void Construct(IAudioFactory audioFactory) 
            => _audioFactory = audioFactory;

        private void Awake() 
            => GetComponent<Button>().onClick.AddListener(() => _audioFactory.Create(AudioSourceType.ButtonClick));
    }
}