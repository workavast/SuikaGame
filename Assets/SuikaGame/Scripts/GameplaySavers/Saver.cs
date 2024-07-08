using System;
using System.Collections.Generic;
using SuikaGame.Scripts.Entities;
using SuikaGame.Scripts.Saves;
using SuikaGame.Scripts.Score;

namespace SuikaGame.Scripts.GameplaySavers
{
    public class Saver : IGameplaySaver
    {
        private readonly IScoreCounter _scoreCounter;
        private readonly IEntitiesRepository _entitiesRepository;

        public event Action OnSave;

        public Saver(IScoreCounter scoreCounter, IEntitiesRepository entitiesRepository)
        {
            _scoreCounter = scoreCounter;
            _entitiesRepository = entitiesRepository;
        }
        
        public void Save()
        {
            PlayerData.Instance.GameplaySceneSettings.SetNewScore(_scoreCounter.Score);
            
            var entityModels = new List<EntityModel>();
            foreach (var entity in _entitiesRepository.Entities)
                if(entity.IsActive)
                    entityModels.Add(entity.GetModel());
            PlayerData.Instance.GameplaySceneSettings.SetNewEntityModels(entityModels);
            
            PlayerData.Instance.GameplaySceneSettings.Apply();
            
            OnSave?.Invoke();
        }
    }
}