using System.Collections;
using System.Collections.Generic;
using System.Linq;
using SuikaGame.Scripts.Entities;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using Zenject;

namespace SuikaGame.Scripts.Skins
{
    [DisallowMultipleComponent]
    public class SkinUpdater : MonoBehaviour
    {
        [SerializeField] private SkinsPacksConfig skinsPacksConfig;
        
        private ISkinPackChanger _skinPackChanger;
        private IEntitiesRepository _entitiesRepository;
        private AssetReference _prevAssetReference;
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
            var targetAssetReference = skinsPacksConfig.SkinsPacks[_skinPackChanger.ActiveSkinPack];
            
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
            if (skinsConfigReference.IsValid())
                yield break;

            var cancelMarker = new CancelMarker(skinsConfigReference);
            _cancelMarkers.Add(cancelMarker);

            var handle = skinsConfigReference.LoadAssetAsync<SkinsPackConfig>();
            yield return handle;

            if (handle.Status == AsyncOperationStatus.Succeeded)
            {
                if (cancelMarker.IsCanceled)
                {
                    Addressables.Release(handle);
                }
                else
                {
                    _currentPackConfig = handle.Result;
                    ApplySprites();
                
                    _prevAssetReference?.ReleaseAsset();
                    _prevAssetReference = skinsConfigReference;
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
            _entitiesRepository.OnAdd -= ApplySprite;
        }
        
        [ContextMenu("Fruits")]
        private void LoadFruits() 
            => _skinPackChanger.ChangeActiveSkinPack(SkinPackType.Fruits);

        [ContextMenu("CATS")]
        private void LoadCats() 
            => _skinPackChanger.ChangeActiveSkinPack(SkinPackType.Cats);

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