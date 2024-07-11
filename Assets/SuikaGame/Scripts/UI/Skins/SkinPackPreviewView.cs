using System;
using SuikaGame.Scripts.AddressablesExtension.AssetReferenceLoading;
using SuikaGame.Scripts.Skins;
using UnityEngine;
using UnityEngine.UI;

namespace SuikaGame.Scripts.UI.Skins
{
    public class SkinPackPreviewView : MonoBehaviour
    {
        [SerializeField] private GameObject loadingTitle;
        [SerializeField] private EntitiesSkinsPreview entitiesSkinsPreview;
        [SerializeField] private Image backgroundPreview;
        [SerializeField] private SkinsPacksConfig skinsPacksConfig;
        
        private AssetReferenceLoaderT<SkinsPackConfig> _assetReferenceLoader;
        private SkinPackPreviewModel _model;

        public void Initialize(SkinPackPreviewModel model)
        {
            _model = model;
            _assetReferenceLoader = new AssetReferenceLoaderT<SkinsPackConfig>(CoroutineParent.Instance);

            _model.OnSkinPackPreviewChanged += SetNewPreview;
            SetNewPreview();
        }
        
        private void SetNewPreview()
        {
            if (!skinsPacksConfig.SkinsPacks.ContainsKey(_model.SkinPackPreview))
                throw new ArgumentOutOfRangeException($"skinsPacksConfig doesnt contains key {_model.SkinPackPreview}");

            loadingTitle.SetActive(true);
            _assetReferenceLoader.Load(skinsPacksConfig.SkinsPacks[_model.SkinPackPreview].SkinPack, ShowPreview);
        }

        private void ShowPreview()
        {
            backgroundPreview.sprite = _assetReferenceLoader.Asset.Background;
            entitiesSkinsPreview.SetNewSkins(_assetReferenceLoader.Asset);
            loadingTitle.SetActive(false);
        }
    }
}