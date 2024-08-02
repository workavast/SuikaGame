using SuikaGame.Scripts.Audio.Sources;
using SuikaGame.Scripts.Audio.Sources.Factory;
using UnityEngine;
using Zenject;

namespace SuikaGame.Scripts.UI.Elements
{
    public class NewRecordTitle : MonoBehaviour
    {
        private IAudioFactory _audioFactory;

        [Inject]
        public void Construct(IAudioFactory audioFactory)
        {
            _audioFactory = audioFactory;
        }
        
        public void Show()
        {
            gameObject.SetActive(true);
            _audioFactory.Create(AudioSourceType.NewRecordCongratulation);
        }

        public void Hide() 
            => gameObject.SetActive(false);
    }
}