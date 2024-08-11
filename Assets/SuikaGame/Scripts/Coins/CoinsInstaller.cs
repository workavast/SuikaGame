using System;
using SuikaGame.Scripts.Saves;
using UnityEngine;
using Zenject;

namespace SuikaGame.Scripts.Coins
{
    public class CoinsInstaller : MonoInstaller
    {
        [SerializeField] private CoinsConfig coinsConfig;
        
        public override void InstallBindings()
        {
            BindConfig();
            BindSettings();
            BindModel();
            BindRewarding();
        }

        private void BindConfig()
        {
            if (coinsConfig == null)
                throw new NullReferenceException("coinsConfig is null");
            
            Container.BindInstance(coinsConfig).AsSingle().NonLazy();
        }
        
        private void BindSettings()
        {
            Container.BindInstance(PlayerData.Instance.CoinsSettings).AsSingle().NonLazy();
        }

        private void BindModel()
        {
            Container.BindInterfacesTo<CoinsModel>().FromNew().AsSingle().NonLazy();
        }
        
        private void BindRewarding()
        {
            Container.BindInterfacesAndSelfTo<CoinsRewarding>().FromNew().AsSingle().NonLazy();
        }
    }
}