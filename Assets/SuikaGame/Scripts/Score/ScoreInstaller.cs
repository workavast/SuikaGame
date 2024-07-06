using System;
using SuikaGame.Scripts.Saves;
using UnityEngine;
using Zenject;

namespace SuikaGame.Scripts.Score
{
    public sealed class ScoreInstaller : MonoInstaller
    {
        [SerializeField] private ScoreConfig scoreConfig;
        
        public override void InstallBindings()
        {
            BindScoreConfig();
            BindScoreCounter();
        }

        private void BindScoreConfig()
        {
            if (scoreConfig == null)
                throw new NullReferenceException($"scoreConfig is null");
            
            Container.BindInstance(scoreConfig).AsSingle();
        }

        private void BindScoreCounter()
        {
            Container.BindInterfacesTo<ScoreCounter>().FromNew().AsSingle()
                .WithArguments(PlayerData.Instance.ScoreSettings).NonLazy();
        }
    }
}