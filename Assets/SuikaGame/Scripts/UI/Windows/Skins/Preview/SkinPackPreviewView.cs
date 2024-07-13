using System;
using SuikaGame.Scripts.AddressablesExtension.AssetReferenceLoading;
using SuikaGame.Scripts.CoroutineHolding;
using SuikaGame.Scripts.Skins.Backgrounds;
using SuikaGame.Scripts.Skins.Entities;
using UnityEngine;
using Zenject;

namespace SuikaGame.Scripts.UI.Windows.Skins.Preview
{
    public class SkinPackPreviewView : MonoBehaviour
    {
        [SerializeField] private GameObject loadingTitle;
        [SerializeField] private EntitiesSkinPackPreview entitiesSkinPackPreview;
        [SerializeField] private BackgroundSkinPreview backgroundPreview;
        
        private EntitiesSkinPacksConfig _entitiesSkinPacksConfig;
        private BackgroundsSkinsConfig _backgroundsSkinsConfig;
        private AssetReferenceLoaderT<EntitiesSkinPackConfig> _assetReferenceLoader;
        private AssetReferenceLoaderT<Sprite> _backgroundAssetReferenceLoader;
        private SkinsPreviewModel _model;

        private bool _entitiesSkinPackIsLoading;
        private bool _backgroundSkinIsLoading;
        
        [Inject]
        public void Construct(CoroutineHolder coroutineHolder, EntitiesSkinPacksConfig entitiesSkinPacksConfig, 
            BackgroundsSkinsConfig backgroundsSkinsConfig)
        {
            _entitiesSkinPacksConfig = entitiesSkinPacksConfig;
            _backgroundsSkinsConfig = backgroundsSkinsConfig;
            
            _assetReferenceLoader = new AssetReferenceLoaderT<EntitiesSkinPackConfig>(coroutineHolder);
            _backgroundAssetReferenceLoader = new AssetReferenceLoaderT<Sprite>(coroutineHolder);
        }
        
        public void Initialize(SkinsPreviewModel model)
        {
            _model = model;

            _model.OnEntitiesSkinPackPreviewChanged += SetNewEntitiesSkinPack;
            _model.OnBackgroundPreviewChanged += SetNewBackgroundSkin;
            
            SetNewEntitiesSkinPack();
            SetNewBackgroundSkin();
        }
        
        private void SetNewEntitiesSkinPack()
        {
            if (!_entitiesSkinPacksConfig.SkinsPacks.ContainsKey(_model.EntitiesSkinPackPreview))
                throw new ArgumentOutOfRangeException($"config doesnt contains key {_model.EntitiesSkinPackPreview}");

            _entitiesSkinPackIsLoading = true;
            loadingTitle.SetActive(true);
            _assetReferenceLoader.Load(_entitiesSkinPacksConfig.SkinsPacks[_model.EntitiesSkinPackPreview].SkinPack, OnEntitiesSkinPackLoaded);
        }
        
        private void SetNewBackgroundSkin()
        {
            if (!_backgroundsSkinsConfig.BackgroundsSkins.ContainsKey(_model.BackgroundSkinPreview))
                throw new ArgumentOutOfRangeException($"config doesnt contains key {_model.BackgroundSkinPreview}");

            _backgroundSkinIsLoading = true;
            loadingTitle.SetActive(true);
            _backgroundAssetReferenceLoader.Load(_backgroundsSkinsConfig.BackgroundsSkins[_model.BackgroundSkinPreview].Background, OnBackgroundSkinLoaded);
        }

        private void OnEntitiesSkinPackLoaded()
        {
            _entitiesSkinPackIsLoading = false;
            TryShowPreview();
        }
        
        private void OnBackgroundSkinLoaded()
        {
            _backgroundSkinIsLoading = false;
            TryShowPreview();
        }
        
        private void TryShowPreview()
        {
            if(_entitiesSkinPackIsLoading || _backgroundSkinIsLoading)
                return;
            
            backgroundPreview.SetNewSkin(_backgroundAssetReferenceLoader.Asset);
            entitiesSkinPackPreview.SetNewSkins(_assetReferenceLoader.Asset);
            loadingTitle.SetActive(false);
        }

        private void OnDestroy()
        {
            if (_model != null)
            {
                _model.OnEntitiesSkinPackPreviewChanged -= SetNewEntitiesSkinPack;
                _model.OnBackgroundPreviewChanged -= SetNewBackgroundSkin;    
            }
            
            _assetReferenceLoader?.Dispose();
            _backgroundAssetReferenceLoader?.Dispose();
        }
    }
}