using SuikaGame.Scripts.AddressablesExtension.AssetReferenceLoading;
using SuikaGame.Scripts.Entities;
using SuikaGame.Scripts.Skins.SkinPackChanging;
using UnityEngine;
using Zenject;

namespace SuikaGame.Scripts.Skins
{
    [DisallowMultipleComponent]
    public class SkinsGameplayUpdaterV2 : MonoBehaviour
    {
        [SerializeField] private SkinsPacksConfig skinsPacksConfig;
        
        private ISkinPackChanger _skinPackChanger;
        private IEntitiesRepository _entitiesRepository;
        private AssetReferenceLoaderT<SkinsPackConfig> _assetReferenceLoader;

        private SkinsPackConfig CurrentPackConfig => _assetReferenceLoader.Asset;
        
        [Inject]
        public void Construct(ISkinPackChanger skinPackChanger, IEntitiesRepository entitiesRepository)
        {
            _skinPackChanger = skinPackChanger;
            _entitiesRepository = entitiesRepository;

            _skinPackChanger.OnActiveSkinPackChanged += ChangeSkin;
            _entitiesRepository.OnAdd += ApplySprite;
        }

        private void Start()
        {
            _assetReferenceLoader = new AssetReferenceLoaderT<SkinsPackConfig>(CoroutineParent.Instance);
            ChangeSkin();
        }

        private void ChangeSkin()
        {
            var targetAssetReference = skinsPacksConfig.SkinsPacks[_skinPackChanger.ActiveSkinPack].SkinPack;
            _assetReferenceLoader.Load(targetAssetReference, ApplySprites);
        }
        
        private void ApplySprites()
        {
            foreach (var entity in _entitiesRepository.Entities)
                ApplySprite(entity);
        }

        private void ApplySprite(Entity entity)
        {
            if(CurrentPackConfig == null)
                return;
            
            if(CurrentPackConfig.Sprites.Count > entity.SizeIndex)
                entity.SetSkin(CurrentPackConfig.Sprites[entity.SizeIndex]);
        }

        private void OnDestroy()
        {
            _assetReferenceLoader.Dispose();
            _entitiesRepository.OnAdd -= ApplySprite;
        }
        
        [ContextMenu("Fruits")]
        private void LoadFruits()
        {
            // var targetAssetReference = skinsPacksConfig.SkinsPacks[SkinPackType.Fruits].SkinPack;
            // _assetReferenceLoader.Load(targetAssetReference, ApplySprites);
            _skinPackChanger.ChangeActiveSkinPack(SkinPackType.Fruits);
        }

        [ContextMenu("CATS")]
        private void LoadCats()
        {
            // var targetAssetReference = skinsPacksConfig.SkinsPacks[SkinPackType.Cats].SkinPack;
            // _assetReferenceLoader.Load(targetAssetReference, ApplySprites);   
            _skinPackChanger.ChangeActiveSkinPack(SkinPackType.Cats);
        }

        [ContextMenu("RELEASE")]
        private void Release() 
            => _assetReferenceLoader.Release();
    }
}