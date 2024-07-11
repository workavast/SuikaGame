using System.Collections;
using System.Collections.Generic;
using System.Linq;
using SuikaGame.Scripts.Entities;
using SuikaGame.Scripts.Skins.SkinPackChanging;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using Zenject;

namespace SuikaGame.Scripts.Skins
{
    [DisallowMultipleComponent]
    public class SkinsGameplayUpdater : MonoBehaviour
    {
        [SerializeField] private SkinsPacksConfig skinsPacksConfig;
        
        private ISkinPackChanger _skinPackChanger;
        private IEntitiesRepository _entitiesRepository;
        private SkinsPackConfig _currentPackConfig;

        private readonly List<CancelMarker> _cancelMarkers = new();
        
        [Inject]
        public void Construct(ISkinPackChanger skinPackChanger, IEntitiesRepository entitiesRepository)
        {
            _skinPackChanger = skinPackChanger;
            _entitiesRepository = entitiesRepository;

            _skinPackChanger.OnActiveSkinPackChanged += ChangeSkin;
            _entitiesRepository.OnAdd += ApplySprite;
        }

        private void Start() 
            => ChangeSkin();
        
        private void ChangeSkin()
        {
            var targetAssetReference = skinsPacksConfig.SkinsPacks[_skinPackChanger.ActiveSkinPack].SkinPack;
            
            if (_cancelMarkers.Count > 0)
            {
                var res = _cancelMarkers.Find(cm => cm.AssetReference == targetAssetReference);
                if (res == null)
                {
                    var lastCancelMarker = _cancelMarkers.Last();
                    lastCancelMarker.IsCanceled = true;
                    StartCoroutine(LoadAsset(targetAssetReference));
                    return;
                }
                else
                {
                    var lastCancelMarker = _cancelMarkers.Last();
                    if(lastCancelMarker.AssetReference != targetAssetReference)
                        lastCancelMarker.IsCanceled = true;
                    
                    res.IsCanceled = false;
                }
            }
        
            StartCoroutine(LoadAsset(targetAssetReference));
        }
        
        private IEnumerator LoadAsset(AssetReferenceT<SkinsPackConfig> skinsConfigReference)
        {
            var cancelMarker = new CancelMarker(skinsConfigReference);
            _cancelMarkers.Add(cancelMarker);
        
            var handle = Addressables.LoadAssetAsync<SkinsPackConfig>(skinsConfigReference);
            yield return handle;
        
            if (handle.Status == AsyncOperationStatus.Succeeded)
            {
                if (cancelMarker.IsCanceled)
                {
                    Addressables.Release(handle);
                }
                else
                {
                    var prevAsset = _currentPackConfig;
                    _currentPackConfig = handle.Result;
                    ApplySprites();
                
                    if(prevAsset != null)
                        Addressables.Release(prevAsset);
                }
            }
            
            _cancelMarkers.Remove(cancelMarker);
        }

        private void ApplySprites()
        {
            foreach (var entity in _entitiesRepository.Entities)
                ApplySprite(entity);
        }

        private void ApplySprite(Entity entity)
        {
            if(_currentPackConfig == null)
                return;
            
            if(_currentPackConfig.Sprites.Count > entity.SizeIndex)
                entity.SetSkin(_currentPackConfig.Sprites[entity.SizeIndex]);
        }

        private void OnDestroy()
        {
            if(_currentPackConfig != null)
                Addressables.Release(_currentPackConfig);
            _entitiesRepository.OnAdd -= ApplySprite;
        }
        
        [ContextMenu("Fruits")]
        private void LoadFruits() 
            => _skinPackChanger.ChangeActiveSkinPack(SkinPackType.Fruits);

        [ContextMenu("CATS")]
        private void LoadCats() 
            => _skinPackChanger.ChangeActiveSkinPack(SkinPackType.Cats);

        [ContextMenu("RELEASE")]
        private void Release() 
            => Addressables.Release(_currentPackConfig);
        
        private class CancelMarker
        {
            public bool IsCanceled;
            public readonly AssetReferenceT<SkinsPackConfig> AssetReference;

            public CancelMarker(AssetReferenceT<SkinsPackConfig> assetReference)
            {
                IsCanceled = false;
                AssetReference = assetReference;
            }
        }
    }
}