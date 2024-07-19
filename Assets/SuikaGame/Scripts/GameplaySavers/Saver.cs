using System;
using System.Collections.Generic;
using SuikaGame.Scripts.Entities;
using SuikaGame.Scripts.GameOverDetection;
using SuikaGame.Scripts.GameplayField;
using SuikaGame.Scripts.Score;

namespace SuikaGame.Scripts.GameplaySavers
{
    public class Saver : IGameplaySaver
    {
        private readonly IScoreCounter _scoreCounter;
        private readonly IEntitiesRepository _entitiesRepository;
        private readonly IGameplayFieldChangeModel _gameplayFieldChangeModel;
        private readonly IGameOverZone _gameOverZone;
        
        public event Action OnSave;

        public Saver(IScoreCounter scoreCounter, IEntitiesRepository entitiesRepository, 
            IGameplayFieldChangeModel gameplayFieldChangeModel, IGameOverZone gameOverZone)
        {
            _scoreCounter = scoreCounter;
            _entitiesRepository = entitiesRepository;
            _gameplayFieldChangeModel = gameplayFieldChangeModel;
            _gameOverZone = gameOverZone;
        }
        
        public void Save()
        {
            var score = 0;
            var entityModels = new List<EntityModel>();
            
            if (!_gameOverZone.GameIsOver)
            {
                score = _scoreCounter.Score;
                foreach (var entity in _entitiesRepository.Entities)
                    if(entity.IsActive)
                        entityModels.Add(entity.GetModel());    
            }
            
            _gameplayFieldChangeModel.SetNewScore(score);
            _gameplayFieldChangeModel.SetNewEntityModels(entityModels);
            _gameplayFieldChangeModel.Apply();
            
            OnSave?.Invoke();
        }
    }
}