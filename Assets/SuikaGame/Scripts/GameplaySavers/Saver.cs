using System;
using System.Collections.Generic;
using SuikaGame.Scripts.Entities;
using SuikaGame.Scripts.GameplayField;
using SuikaGame.Scripts.Saves;
using SuikaGame.Scripts.Score;

namespace SuikaGame.Scripts.GameplaySavers
{
    public class Saver : IGameplaySaver
    {
        private readonly IScoreCounter _scoreCounter;
        private readonly IEntitiesRepository _entitiesRepository;
        private readonly IGameplayFieldChangeModel _gameplayFieldChangeModel;

        public event Action OnSave;

        public Saver(IScoreCounter scoreCounter, IEntitiesRepository entitiesRepository, 
            IGameplayFieldChangeModel gameplayFieldChangeModel)
        {
            _scoreCounter = scoreCounter;
            _entitiesRepository = entitiesRepository;
            _gameplayFieldChangeModel = gameplayFieldChangeModel;
        }
        
        public void Save()
        {
            _gameplayFieldChangeModel.SetNewScore(_scoreCounter.Score);
            
            var entityModels = new List<EntityModel>();
            foreach (var entity in _entitiesRepository.Entities)
                if(entity.IsActive)
                    entityModels.Add(entity.GetModel());
            _gameplayFieldChangeModel.SetNewEntityModels(entityModels);
            
            _gameplayFieldChangeModel.Apply();
            
            OnSave?.Invoke();
        }
    }
}