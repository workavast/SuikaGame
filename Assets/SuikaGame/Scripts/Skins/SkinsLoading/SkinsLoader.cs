using System;
using SuikaGame.Scripts.AddressablesExtension.AssetReferenceLoading;
using SuikaGame.Scripts.CoroutineHolding;
using SuikaGame.Scripts.Skins.Backgrounds;
using SuikaGame.Scripts.Skins.Entities;
using UnityEngine;

namespace SuikaGame.Scripts.Skins.SkinsLoading
{
    public class SkinsLoader : ISkinsLoader, IDisposable
    {
        private readonly AssetReferenceLoaderT<EntitiesSkinPackConfig> _entitiesSkinPackLoader;
        private readonly AssetReferenceLoaderT<Sprite> _backgroundSkinLoader;
        private readonly BackgroundsSkinsConfig _backgroundsSkinsConfig;
        private readonly EntitiesSkinPacksConfig _entitiesSkinPacksConfig;
        private readonly ISkinsChanger _skinsChanger;

        public EntitiesSkinPackConfig EntitiesSkinPackConfig => _entitiesSkinPackLoader.Asset;
        public Sprite BackgroundSkin => _backgroundSkinLoader.Asset;

        public event Action OnEntitiesSkinsLoaded;
        public event Action OnBackgroundSkinLoaded;
        
        //DONT Swap CoroutineHolder to MonoBehaviour, it used for injection by zenject
        public SkinsLoader(CoroutineHolder coroutineHolder, EntitiesSkinPacksConfig entitiesSkinPacksConfig,
            BackgroundsSkinsConfig backgroundsSkinsConfig, ISkinsChanger skinsChanger)
        {
            _entitiesSkinPackLoader = new AssetReferenceLoaderT<EntitiesSkinPackConfig>(coroutineHolder);
            _backgroundSkinLoader = new AssetReferenceLoaderT<Sprite>(coroutineHolder);

            _entitiesSkinPacksConfig = entitiesSkinPacksConfig;
            _backgroundsSkinsConfig = backgroundsSkinsConfig;
            _skinsChanger = skinsChanger;

            _skinsChanger.OnEntitiesSkinPackChanged += LoadEntitiesSkinPack;
            _skinsChanger.OnBackgroundSkinChanged += LoadBackgroundSkin;
            
            if(_skinsChanger.IsEntitiesSkinPackInitialized)
                LoadEntitiesSkinPack();
            if(_skinsChanger.IsBackgroundSkinInitialized)
                LoadBackgroundSkin();
        }

        private void LoadEntitiesSkinPack()
        {
            var assetReference = _entitiesSkinPacksConfig.SkinsPacks[_skinsChanger.ActiveEntitiesSkinPack].SkinPack;
            _entitiesSkinPackLoader.Load(assetReference, SendEntitiesMessage);
        }

        private void LoadBackgroundSkin()
        {
            var assetReference = _backgroundsSkinsConfig.BackgroundsSkins[_skinsChanger.ActiveBackgroundSkin].Background;
            _backgroundSkinLoader.Load(assetReference, SendBackgroundMessage);
        }

        private void SendEntitiesMessage() 
            => OnEntitiesSkinsLoaded?.Invoke();

        private void SendBackgroundMessage() 
            => OnBackgroundSkinLoaded?.Invoke();
        
        public void Dispose()
        {
            _skinsChanger.OnEntitiesSkinPackChanged -= LoadEntitiesSkinPack;
            _skinsChanger.OnBackgroundSkinChanged -= LoadBackgroundSkin;
            
            _entitiesSkinPackLoader?.Dispose();
            _backgroundSkinLoader?.Dispose();
        }
    }
}