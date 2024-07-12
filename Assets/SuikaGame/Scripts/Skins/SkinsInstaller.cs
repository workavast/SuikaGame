using System;
using SuikaGame.Scripts.Saves;
using SuikaGame.Scripts.Skins.Backgrounds;
using SuikaGame.Scripts.Skins.Entities;
using SuikaGame.Scripts.Skins.SkinsChanging;
using SuikaGame.Scripts.Skins.SkinsLoading;
using UnityEngine;
using Zenject;

namespace SuikaGame.Scripts.Skins
{
    public class SkinsInstaller : MonoInstaller
    {
        [SerializeField] private EntitiesSkinPacksConfig entitiesSkinPacksConfig;
        [SerializeField] private BackgroundsSkinsConfig backgroundsSkinsConfig;
        
        public override void InstallBindings()
        {
            BindConfig();
            BindBackgroundsSkinsConfig();
            BindSkinsChanger();
            BindSkinPackLoader();
        }

        private void BindConfig()
        {
            if (entitiesSkinPacksConfig == null)
                throw new NullReferenceException("skinsPacksConfig == null");
            
            Container.BindInstance(entitiesSkinPacksConfig).AsSingle().NonLazy();
        }

        private void BindBackgroundsSkinsConfig()
        {
            if (backgroundsSkinsConfig == null)
                throw new NullReferenceException("backgroundsSkinsConfig == null");
            
            Container.BindInstance(backgroundsSkinsConfig).AsSingle().NonLazy();
        }
        
        private void BindSkinsChanger()
        {
            Container.BindInterfacesTo<SkinsChanger>().FromNew().AsSingle().WithArguments(PlayerData.Instance.SkinsPacksSettings).NonLazy();
        }

        private void BindSkinPackLoader()
        {
            Container.BindInterfacesTo<SkinsLoader>().FromNew().AsSingle().NonLazy();
        }
    }
}