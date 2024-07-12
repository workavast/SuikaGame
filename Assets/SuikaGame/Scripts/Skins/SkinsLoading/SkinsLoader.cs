using System;
using SuikaGame.Scripts.AddressablesExtension.AssetReferenceLoading;
using SuikaGame.Scripts.CoroutineHolding;
using SuikaGame.Scripts.Skins.Backgrounds;
using SuikaGame.Scripts.Skins.Entities;
using SuikaGame.Scripts.Skins.SkinsChanging;
using UnityEngine;

namespace SuikaGame.Scripts.Skins.SkinsLoading
{
    public class SkinsLoader : ISkinsLoader, IDisposable
    {
        private readonly AssetReferenceLoaderT<EntitiesSkinPackConfig> _entitiesSkinPackLoader;
        private readonly AssetReferenceLoaderT<Sprite> _backgroundSkinLoader;
        private readonly BackgroundsSkinsConfig _backgroundsSkinsConfig;
        private readonly EntitiesSkinPacksConfig _entitiesSkinPackPacksConfig;
        private readonly ISkinsChanger _skinsChanger;

        public EntitiesSkinPackConfig EntitiesSkinPackConfig => _entitiesSkinPackLoader.Asset;
        public Sprite BackgroundSkin => _backgroundSkinLoader.Asset;

        public event Action OnEntitiesSkinsLoaded;
        public event Action OnBackgroundSkinLoaded;
        
        //DONT Swap CoroutineHolder to MonoBehaviour
        public SkinsLoader(CoroutineHolder coroutineHolder, EntitiesSkinPacksConfig entitiesSkinPackPacksConfig,
            BackgroundsSkinsConfig backgroundsSkinsConfig, ISkinsChanger skinsChanger)
        {
            _entitiesSkinPackLoader = new AssetReferenceLoaderT<EntitiesSkinPackConfig>(coroutineHolder);
            _backgroundSkinLoader = new AssetReferenceLoaderT<Sprite>(coroutineHolder);

            _entitiesSkinPackPacksConfig = entitiesSkinPackPacksConfig;
            _backgroundsSkinsConfig = backgroundsSkinsConfig;
            _skinsChanger = skinsChanger;

            _skinsChanger.OnActiveEntitySkinChanged += LoadEntitiesSkinPack;
            _skinsChanger.OnActiveBackgroundChanged += LoadBackgroundSkin;

            LoadEntitiesSkinPack();
            LoadBackgroundSkin();
        }
        
        private void LoadEntitiesSkinPack()
        {
            var targetAssetReference = _entitiesSkinPackPacksConfig.SkinsPacks[_skinsChanger.ActiveEntitiesSkinPack].SkinPack;
            _entitiesSkinPackLoader.Load(targetAssetReference, SendEntitiesMessage);
        }

        private void LoadBackgroundSkin()
        {
            var targetAssetReference =
                _backgroundsSkinsConfig.BackgroundsSkins[_skinsChanger.ActiveBackgroundSkin].Background;
            _backgroundSkinLoader.Load(targetAssetReference, SendBackgroundMessage);
        }

        private void SendEntitiesMessage() 
            => OnEntitiesSkinsLoaded?.Invoke();

        private void SendBackgroundMessage() 
            => OnBackgroundSkinLoaded?.Invoke();
        
        public void Dispose()
        {
            _entitiesSkinPackLoader?.Dispose();
            _backgroundSkinLoader?.Dispose();
        }
    }
}