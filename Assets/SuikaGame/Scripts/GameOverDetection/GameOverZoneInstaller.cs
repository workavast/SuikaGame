using System;
using UnityEngine;
using Zenject;

namespace SuikaGame.Scripts.GameOverDetection
{
    public class GameOverZoneInstaller : MonoInstaller
    {
        [SerializeField] private GameOverZoneConfig config;
        
        public override void InstallBindings()
        {
            BindConfig();
            BindZone();
        }

        private void BindConfig()
        {
            if (config == null)
                throw new NullReferenceException($"config is null");

            Container.BindInstance(config).AsSingle();
        }

        private void BindZone()
        {
            var gameOverZone = FindObjectOfType<GameOverZone>();
            if (gameOverZone == null)
                throw new NullReferenceException($"gameOverZone is null");

            Container.BindInterfacesTo<GameOverZone>().FromInstance(gameOverZone).AsSingle();
        }
    }
}