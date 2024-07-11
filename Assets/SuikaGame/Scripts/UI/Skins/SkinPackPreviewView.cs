using System;
using SuikaGame.Scripts.AddressablesExtension.AssetReferenceLoading;
using SuikaGame.Scripts.CoroutineHolding;
using SuikaGame.Scripts.Skins;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace SuikaGame.Scripts.UI.Skins
{
    public class SkinPackPreviewView : MonoBehaviour
    {
        [SerializeField] private GameObject loadingTitle;
        [SerializeField] private EntitiesSkinsPreview entitiesSkinsPreview;
        [SerializeField] private Image backgroundPreview;
        
        [Inject] private SkinsPacksConfig _skinsPacksConfig;
        
        private AssetReferenceLoaderT<SkinsPackConfig> _assetReferenceLoader;
        private SkinPackPreviewModel _model;

        public void Initialize(SkinPackPreviewModel model)
        {
            _model = model;
            _assetReferenceLoader = new AssetReferenceLoaderT<SkinsPackConfig>(CoroutineHolder.Instance);

            _model.OnSkinPackPreviewChanged += SetNewPreview;
            SetNewPreview();
        }
        
        private void SetNewPreview()
        {
            if (!_skinsPacksConfig.SkinsPacks.ContainsKey(_model.SkinPackPreview))
                throw new ArgumentOutOfRangeException($"skinsPacksConfig doesnt contains key {_model.SkinPackPreview}");

            loadingTitle.SetActive(true);
            _assetReferenceLoader.Load(_skinsPacksConfig.SkinsPacks[_model.SkinPackPreview].SkinPack, ShowPreview);
        }

        private void ShowPreview()
        {
            backgroundPreview.sprite = _assetReferenceLoader.Asset.Background;
            entitiesSkinsPreview.SetNewSkins(_assetReferenceLoader.Asset);
            loadingTitle.SetActive(false);
        }

        private void OnDestroy()
        {
            _assetReferenceLoader.Dispose();
        }
    }
}