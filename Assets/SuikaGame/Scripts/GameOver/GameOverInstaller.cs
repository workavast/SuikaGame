using System;
using SuikaGame.Scripts.GameOver.GameOverControlling;
using SuikaGame.Scripts.GameOver.GameOverDetection;
using UnityEngine;
using Zenject;

namespace SuikaGame.Scripts.GameOver
{
    public class GameOverInstaller : MonoInstaller
    {
        [SerializeField] private GameOverZoneConfig config;
        
        public override void InstallBindings()
        {
            BindConfig();
            BindZone();
            BindController();
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
        
        private void BindController()
        {
            Container.BindInterfacesTo<GameOverController>().FromNew().AsSingle();
        }
    }
}