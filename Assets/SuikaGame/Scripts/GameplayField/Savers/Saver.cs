using System;
using System.Collections.Generic;
using SuikaGame.Scripts.Entities;
using SuikaGame.Scripts.GameOver.GameOverControlling;
using SuikaGame.Scripts.GameplayControlling;
using SuikaGame.Scripts.GameplayField.Model;
using SuikaGame.Scripts.Score;

namespace SuikaGame.Scripts.GameplayField.Savers
{
    public class Saver : IGameplaySaver
    {
        private readonly IScoreCounter _scoreCounter;
        private readonly IEntitiesRepository _entitiesRepository;
        private readonly IGameplayFieldChangeModel _gameplayFieldChangeModel;
        private readonly IGameOverProvider _gameOverProvider;
        
        public event Action OnSave;

        public Saver(IScoreCounter scoreCounter, IEntitiesRepository entitiesRepository, 
            IGameplayFieldChangeModel gameplayFieldChangeModel, IGameOverProvider gameOverProvider)
        {
            _scoreCounter = scoreCounter;
            _entitiesRepository = entitiesRepository;
            _gameplayFieldChangeModel = gameplayFieldChangeModel;
            _gameOverProvider = gameOverProvider;
        }
        
        public void Save()
        {
            var score = 0;
            var entityModels = new List<EntityModel>();
            
            if (!_gameOverProvider.GameIsOver)
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