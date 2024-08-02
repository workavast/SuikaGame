using SuikaGame.Scripts.Audio.Sources;
using SuikaGame.Scripts.Audio.Sources.Factory;
using SuikaGame.Scripts.Vfx;
using UnityEngine;
using Zenject;

namespace SuikaGame.Scripts.GameplayField.Hiding
{
    public class GameplayFieldVfxHider : MonoBehaviour
    {
        [SerializeField] private float vfxScale;
        
        private IVfxFactory _vfxFactory;
        private IAudioFactory _audioFactory;

        [Inject]
        public void Construct(IVfxFactory vfxFactory, IAudioFactory audioFactory)
        {
            _vfxFactory = vfxFactory;
            _audioFactory = audioFactory;
        }

        public void HideGameField()
        {
            _vfxFactory.Create(VfxType.Smoke, transform.position, vfxScale);
            _audioFactory.Create(AudioSourceType.MergeEffect, transform.position);
        }
    }
}