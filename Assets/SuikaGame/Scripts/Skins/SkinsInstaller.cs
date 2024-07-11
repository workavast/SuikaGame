using System;
using SuikaGame.Scripts.Saves;
using SuikaGame.Scripts.Skins.SkinPackChanging;
using SuikaGame.Scripts.Skins.SkinPackLoading;
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
            BindSkinPackLoader();
        }

        private void BindConfig()
        {
            if (skinsPacksConfig == null)
                throw new NullReferenceException("skinsPacksConfig == null");
            
            Container.BindInstance(skinsPacksConfig).AsSingle().NonLazy();
        }

        private void BindSkinsChanger()
        {
            Container.BindInterfacesTo<SkinPackChanger>().FromNew().AsSingle().WithArguments(PlayerData.Instance.SkinsPacksSettings).NonLazy();
        }

        private void BindSkinPackLoader()
        {
            Container.BindInterfacesTo<SkinPackLoader>().FromNew().AsSingle().NonLazy();
        }
    }
}