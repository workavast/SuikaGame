using System;
using SuikaGame.Scripts.AddressablesExtension.AssetReferenceLoading;
using SuikaGame.Scripts.CoroutineHolding;
using SuikaGame.Scripts.Skins.SkinPackChanging;

namespace SuikaGame.Scripts.Skins.SkinPackLoading
{
    public class SkinPackLoader : ISkinPackLoader
    {
        private readonly SkinsPacksConfig _skinsPacksConfig;
        private readonly ISkinPackChanger _skinPackChanger;
        private readonly AssetReferenceLoaderT<SkinsPackConfig> _assetReferenceLoader;

        public SkinsPackConfig PackConfig => _assetReferenceLoader.Asset;

        public event Action OnSkinPackLoaded;
        
        //DONT Swap CoroutineHolder to MonoBehaviour
        public SkinPackLoader(CoroutineHolder coroutineHolder, SkinsPacksConfig skinsPacksConfig, ISkinPackChanger skinPackChanger)
        {
            _assetReferenceLoader = new AssetReferenceLoaderT<SkinsPackConfig>(coroutineHolder);
            _skinsPacksConfig = skinsPacksConfig;
            _skinPackChanger = skinPackChanger;

            _skinPackChanger.OnActiveSkinPackChanged += LoadSkinPack;
            
            LoadSkinPack();
        }

        private void LoadSkinPack()
        {
            var targetAssetReference = _skinsPacksConfig.SkinsPacks[_skinPackChanger.ActiveSkinPack].SkinPack;
            _assetReferenceLoader.Load(targetAssetReference, SendMessage);
        }

        private void SendMessage() 
            => OnSkinPackLoaded?.Invoke();

        private void OnDestroy() 
            => _assetReferenceLoader.Dispose();
    }
}