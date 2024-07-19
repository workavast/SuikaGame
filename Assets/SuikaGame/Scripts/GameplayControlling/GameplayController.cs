using Avastrad.UI.UI_System;
using SuikaGame.Scripts.Coins;
using SuikaGame.Scripts.Entities;
using SuikaGame.Scripts.Entities.Spawning;
using SuikaGame.Scripts.EntityMaxSizeCounting;
using SuikaGame.Scripts.GameOverDetection;
using SuikaGame.Scripts.GameplaySavers;
using SuikaGame.Scripts.Score;
using UnityEngine;
using Zenject;

namespace SuikaGame.Scripts.GameplayControlling
{
    public class GameplayController : IGameplayController, IGameReseter
    {
        private IEntitySpawner _entitySpawner;
        private IEntitiesRepository _entitiesRepository;
        private IScoreCounter _scoreCounter;
        private IEntityMaxSizeCounter _entityMaxSizeCounter;
        private IGameOverZone _gameOverZone;
        private IGameplaySaver _gameplaySaver;
        private ICoinsController _coinsController;
        
        [Inject]
        public void Construct(IEntitySpawner entitySpawner, IEntitiesRepository entitiesRepository, 
            IScoreCounter scoreCounter, IEntityMaxSizeCounter entityMaxSizeCounter, IGameOverZone gameOverZone,
            IGameplaySaver gameplaySaver, ICoinsController coinsController)
        {
            _entitySpawner = entitySpawner;
            _entitiesRepository = entitiesRepository;
            _entityMaxSizeCounter = entityMaxSizeCounter;
            _scoreCounter = scoreCounter;
            _gameOverZone = gameOverZone;
            _gameplaySaver = gameplaySaver;
            _coinsController = coinsController;

            _gameOverZone.OnGameIsOver += OnGameIsOver;
        }

        public void Initialize()
        {
            _entitySpawner.Initialize();
        }
        
        public void ResetGame()
        {
            if (!_gameOverZone.GameIsOver) 
                ApplySessionResult();

            Debug.Log($"RESET");
            _gameOverZone.Reset();
            _scoreCounter.Reset();
            _entityMaxSizeCounter.Reset();
            _entitiesRepository.Reset();
            _entitySpawner.Reset();
            UI_Controller.SetSingleScreen(ScreenType.Gameplay);
            
            Initialize();
            _gameplaySaver.Save();
        }
        
        private void ApplySessionResult()
        {
            _coinsController.AddCoinsByScore(_scoreCounter.Score);
            if (_scoreCounter.Record <= _scoreCounter.Score) 
                _scoreCounter.ApplyRecord();
        }

        private void OnGameIsOver()
        {
            ApplySessionResult();
            UI_Controller.ToggleScreen(ScreenType.GameplayEnd, true);
            _gameplaySaver.Save();
        }
    }
}