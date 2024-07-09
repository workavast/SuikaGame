using System;
using SuikaGame.Scripts.Saves;
using UnityEngine;
using Zenject;

namespace SuikaGame.Scripts.Skins
{
    public class SkinsInstaller : MonoInstaller
    {
        [SerializeField] private SkinsPacksConfig skinsPacksConfig;
        
        public override void InstallBindings()
        {
            BindConfig();
            BindSkinsChanger();
        }

        private void BindConfig()
        {
            if (skinsPacksConfig == null)
                throw new NullReferenceException("skinsPacksConfig == null");
            
            Container.BindInstance(skinsPacksConfig).AsSingle();
        }

        private void BindSkinsChanger()
        {
            Container.BindInterfacesTo<SkinPackChanger>().FromNew().AsSingle().WithArguments(PlayerData.Instance.SkinsPacksSettings).NonLazy();
        }
    }
}