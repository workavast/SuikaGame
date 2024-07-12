using System;
using SuikaGame.Scripts.Entities;
using SuikaGame.Scripts.Skins.SkinsLoading;

namespace SuikaGame.Scripts.Skins.Entities.SkinsUpdating
{
    public class EntitiesSkinsUpdater : IDisposable
    {
        private readonly ISkinsLoader _skinsLoader;
        private readonly IEntitiesRepository _entitiesRepository;
        
        public EntitiesSkinsUpdater(ISkinsLoader skinsLoader, IEntitiesRepository entitiesRepository)
        {
            _skinsLoader = skinsLoader;
            _entitiesRepository = entitiesRepository;

            _skinsLoader.OnEntitiesSkinsLoaded += SetEntitiesSkins;
            _entitiesRepository.OnAdd += ApplySprite;
            
            SetEntitiesSkins();
        }

        private void SetEntitiesSkins() 
        {
            if(_skinsLoader.EntitiesSkinPackConfig == null)
                return;
            
            foreach (var entity in _entitiesRepository.Entities)
                ApplySprite(entity);   
        }

        private void ApplySprite(Entity entity)
        {
            if(_skinsLoader.EntitiesSkinPackConfig == null)
                return;
            
            if(_skinsLoader.EntitiesSkinPackConfig.Skins.Count > entity.SizeIndex)
                entity.SetSkin(_skinsLoader.EntitiesSkinPackConfig.Skins[entity.SizeIndex]);
        }

        public void Dispose()
        {
            _skinsLoader.OnEntitiesSkinsLoaded -= SetEntitiesSkins;
            _entitiesRepository.OnAdd -= ApplySprite;
        }
    }
}