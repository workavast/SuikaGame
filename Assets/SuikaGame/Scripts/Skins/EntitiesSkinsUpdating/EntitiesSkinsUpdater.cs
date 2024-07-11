using System;
using SuikaGame.Scripts.Entities;
using SuikaGame.Scripts.Skins.SkinPackLoading;

namespace SuikaGame.Scripts.Skins.EntitiesSkinsUpdating
{
    public class EntitiesSkinsUpdater : IDisposable
    {
        private readonly ISkinPackLoader _skinPackLoader;
        private readonly IEntitiesRepository _entitiesRepository;
        
        public EntitiesSkinsUpdater(ISkinPackLoader skinPackLoader, IEntitiesRepository entitiesRepository)
        {
            _skinPackLoader = skinPackLoader;
            _entitiesRepository = entitiesRepository;

            _skinPackLoader.OnSkinPackLoaded += SetSkinPack;
            _entitiesRepository.OnAdd += ApplySprite;
        }

        private void SetSkinPack() 
        {
            foreach (var entity in _entitiesRepository.Entities)
                ApplySprite(entity);   
        }

        private void ApplySprite(Entity entity)
        {
            if(_skinPackLoader.PackConfig == null)
                return;
            
            if(_skinPackLoader.PackConfig.Sprites.Count > entity.SizeIndex)
                entity.SetSkin(_skinPackLoader.PackConfig.Sprites[entity.SizeIndex]);
        }

        public void Dispose()
        {
            _entitiesRepository.OnAdd -= ApplySprite;
        }
    }
}