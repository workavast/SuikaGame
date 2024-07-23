using System;
using SuikaGame.Scripts.AddressablesExtension.AssetReferenceLoading;
using SuikaGame.Scripts.CoroutineHolding;
using SuikaGame.Scripts.Skins.Backgrounds;
using UnityEngine;
using Zenject;

namespace SuikaGame.Scripts.UI.Windows.Skins.Preview
{
    public class BackgroundPreviewView : MonoBehaviour
    {
        [SerializeField] private LoadingTitle loadingTitle;
        [SerializeField] private BackgroundSkinPreview backgroundPreview;
        
        private BackgroundsSkinsConfig _backgroundsSkinsConfig;
        private AssetReferenceLoaderT<Sprite> _backgroundAssetReferenceLoader;
        private SkinsPreviewModel _model;

        [Inject]
        public void Construct(CoroutineHolder coroutineHolder, BackgroundsSkinsConfig backgroundsSkinsConfig)
        {
            _backgroundsSkinsConfig = backgroundsSkinsConfig;
            
            _backgroundAssetReferenceLoader = new AssetReferenceLoaderT<Sprite>(coroutineHolder);
        }
        
        public void Initialize(SkinsPreviewModel model)
        {
            _model = model;

            _model.OnBackgroundPreviewChanged += SetNewBackgroundSkin;
            
            SetNewBackgroundSkin(_model.BackgroundSkinPreview);
        }
        
        private void SetNewBackgroundSkin(BackgroundSkinType newSkinType)
        {
            if (!_backgroundsSkinsConfig.BackgroundsSkins.ContainsKey(newSkinType))
                throw new ArgumentOutOfRangeException($"config doesnt contains key {newSkinType}");

            loadingTitle.Show();
            _backgroundAssetReferenceLoader.Load(_backgroundsSkinsConfig.BackgroundsSkins[newSkinType].Background, ShowPreview);
        }

        private void ShowPreview()
        {
            backgroundPreview.SetNewSkin(_backgroundAssetReferenceLoader.Asset);
            loadingTitle.Hide();
        }

        private void OnDestroy()
        {
            if (_model != null) 
                _model.OnBackgroundPreviewChanged -= SetNewBackgroundSkin;

            _backgroundAssetReferenceLoader?.Dispose();
        }     
    }
}