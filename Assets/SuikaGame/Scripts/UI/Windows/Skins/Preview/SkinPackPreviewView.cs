using System;
using SuikaGame.Scripts.AddressablesExtension.AssetReferenceLoading;
using SuikaGame.Scripts.CoroutineHolding;
using SuikaGame.Scripts.Skins.Entities;
using UnityEngine;
using Zenject;

namespace SuikaGame.Scripts.UI.Windows.Skins.Preview
{
    public class SkinPackPreviewView : MonoBehaviour
    {
        [SerializeField] private LoadingTitle loadingTitle;
        [SerializeField] private EntitiesSkinPackPreview entitiesSkinPackPreview;
        
        private EntitiesSkinPacksConfig _entitiesSkinPacksConfig;
        private AssetReferenceLoaderT<EntitiesSkinPackConfig> _assetReferenceLoader;
        private SkinsPreviewModel _model;

        [Inject]
        public void Construct(CoroutineHolder coroutineHolder, EntitiesSkinPacksConfig entitiesSkinPacksConfig)
        {
            _entitiesSkinPacksConfig = entitiesSkinPacksConfig;
            
            _assetReferenceLoader = new AssetReferenceLoaderT<EntitiesSkinPackConfig>(coroutineHolder);
        }
        
        public void Initialize(SkinsPreviewModel model)
        {
            _model = model;

            _model.OnEntitiesSkinPackPreviewChanged += SetNewEntitiesSkinPack;
            
            SetNewEntitiesSkinPack(_model.EntitiesSkinPackPreview);
        }
        
        private void SetNewEntitiesSkinPack(EntitiesSkinPackType newSkinPackType)
        {
            if (!_entitiesSkinPacksConfig.SkinsPacks.ContainsKey(newSkinPackType))
                throw new ArgumentOutOfRangeException($"config doesnt contains key {newSkinPackType}");

            loadingTitle.Show();
            _assetReferenceLoader.Load(_entitiesSkinPacksConfig.SkinsPacks[newSkinPackType].SkinPack, ShowPreview);
        }
        
        private void ShowPreview()
        {
            entitiesSkinPackPreview.SetNewSkins(_assetReferenceLoader.Asset);
            loadingTitle.Hide();
        }

        private void OnDestroy()
        {
            if (_model != null) 
                _model.OnEntitiesSkinPackPreviewChanged -= SetNewEntitiesSkinPack;

            _assetReferenceLoader?.Dispose();
        }
    }
}