using SuikaGame.Scripts.Audio;
using SuikaGame.Scripts.Saves;
using UnityEngine;
using UnityEngine.Audio;
using Zenject;

namespace SuikaGame.Scripts.Bootstraps
{
    public class GameContextsInstaller : MonoInstaller
    {
        [SerializeField] private AudioMixer audioMixer;

        public override void InstallBindings()
        {
            BindAudioVolumeChanger();
        }
        
        private void BindAudioVolumeChanger()
        {
            Container.Bind<AudioVolumeChanger>().FromNew().AsSingle()
                .WithArguments(audioMixer, PlayerData.Instance.VolumeSettings).NonLazy();
        }
    }
}