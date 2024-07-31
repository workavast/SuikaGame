using System;
using SuikaGame.Scripts.Audio.Sources.Factory;
using UnityEngine;
using Zenject;

namespace SuikaGame.Scripts.Audio.Sources
{
    public class AudioInstaller : MonoInstaller
    {
        [SerializeField] private AudioSourcesConfig audioSourcesConfig;
        [SerializeField] private BallsCollisionConfig ballsCollisionConfig;
        
        public override void InstallBindings()
        {
            BindConfig();
            BindFactory();
        }

        private void BindConfig()
        {
            if (audioSourcesConfig == null)
                throw new NullReferenceException($"config is null");

            if (ballsCollisionConfig == null)
                throw new NullReferenceException($"config is null");
            
            Container.BindInstance(audioSourcesConfig).AsSingle();
            Container.BindInstance(ballsCollisionConfig).AsSingle();
        }

        private void BindFactory()
        {
            var factory = FindObjectOfType<AudioFactory>(true);
            if (factory == null)
                throw new NullReferenceException($"factory is null");

            Container.BindInterfacesTo<AudioFactory>().FromInstance(factory).AsSingle();
        }
    }
}