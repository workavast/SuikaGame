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
            BindController();
        }

        private void BindConfig()
        {
            if (coinsConfig == null)
                throw new NullReferenceException("coinsConfig is null");
            
            Container.BindInstance(coinsConfig).AsSingle().NonLazy();
        }
        
        private void BindSettings()
        {
            Container.BindInterfacesAndSelfTo<ICoinsModel>().FromInstance(PlayerData.Instance.CoinsSettings).AsSingle().NonLazy();
        }

        private void BindController()
        {
            Container.BindInterfacesTo<CoinsController>().FromNew().AsSingle().NonLazy();
        }
    }
}