using SuikaGame.Scripts.AddressablesExtension.AssetReferenceLoading;
using SuikaGame.Scripts.Entities;
using SuikaGame.Scripts.Skins.SkinPackChanging;
using UnityEngine;
using UnityEngine.AddressableAssets;
using Zenject;

namespace SuikaGame.Scripts.Skins
{
    [DisallowMultipleComponent]
    public class SkinsGameplayUpdaterV3 : MonoBehaviour
    {
        [SerializeField] private SkinsPacksConfig skinsPacksConfig;
        
        private ISkinPackChanger _skinPackChanger;
        private IEntitiesRepository _entitiesRepository;
        private SkinsPackConfig _currentPackConfig;

        private AssetReferenceLoaderT<SkinsPackConfig> _assetReferenceLoader;
        
        [Inject]
        public void Construct(ISkinPackChanger skinPackChanger, IEntitiesRepository entitiesRepository)
        {
            _skinPackChanger = skinPackChanger;
            _entitiesRepository = entitiesRepository;

            _skinPackChanger.OnActiveSkinPackChanged += ChangeSkin;
        }

        private void Start()
        {
            _assetReferenceLoader = new AssetReferenceLoaderT<SkinsPackConfig>(CoroutineParent.Instance);
            ChangeSkin();
        }

        private void ChangeSkin()
        {
            var targetAssetReference = skinsPacksConfig.SkinsPacks[_skinPackChanger.ActiveSkinPack].SkinPack;
            _assetReferenceLoader.Load(targetAssetReference);
        }

        private void OnDestroy()
        {
            _assetReferenceLoader.Dispose();
        }
        
        [ContextMenu("Fruits")]
        private void LoadFruits()
        {
            var targetAssetReference = skinsPacksConfig.SkinsPacks[SkinPackType.Fruits].SkinPack;
            _assetReferenceLoader.Load(targetAssetReference);
            // _skinPackChanger.ChangeActiveSkinPack(SkinPackType.Fruits);
        }

        [ContextMenu("CATS")]
        private void LoadCats()
        {
            var targetAssetReference = skinsPacksConfig.SkinsPacks[SkinPackType.Cats].SkinPack;
            _assetReferenceLoader.Load(targetAssetReference);   
            // _skinPackChanger.ChangeActiveSkinPack(SkinPackType.Cats);
        }

        [ContextMenu("RELEASE")]
        private void Release() 
            => _assetReferenceLoader.Release();
    }
}