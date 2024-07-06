using SuikaGame.Scripts.Audio;
using SuikaGame.Scripts.Saves;
using UnityEngine;
using UnityEngine.Audio;
using Zenject;

namespace SuikaGame.Scripts.Bootstraps
{
    public class AudioVolumeChangerInstaller : MonoInstaller
    {
        [SerializeField] private AudioMixer audioMixer;

        public override void InstallBindings()
        {
            Container.Bind<AudioVolumeChanger>().FromNew().AsSingle()
                .WithArguments(audioMixer, PlayerData.Instance.VolumeSettings).NonLazy();
        }
    }
}