using Avastrad.UI.UI_System;
using SuikaGame.Scripts.Coins;
using SuikaGame.Scripts.Entities;
using SuikaGame.Scripts.Entities.MaxSizeCounting;
using SuikaGame.Scripts.Entities.Spawning;
using SuikaGame.Scripts.GameOverDetection;
using SuikaGame.Scripts.GameplayField.Hiding;
using SuikaGame.Scripts.GameplayField.Savers;
using SuikaGame.Scripts.Score;
using UnityEngine;

namespace SuikaGame.Scripts.GameplayControlling
{
    public class GameplayController : IGameplayController, IGameReseter
    {
        private readonly IEntitySpawner _entitySpawner;
        private readonly IEntitiesRepository _entitiesRepository;
        private readonly IScoreCounter _scoreCounter;
        private readonly IEntityMaxSizeCounter _entityMaxSizeCounter;
        private readonly IGameOverZone _gameOverZone;
        private readonly IGameplaySaver _gameplaySaver;
        private readonly ICoinsModel _coinsModel;
        private readonly GameplayFieldVfxHider _gameplayFieldVfxHider;

        public GameplayController(IEntitySpawner entitySpawner, IEntitiesRepository entitiesRepository, 
            IScoreCounter scoreCounter, IEntityMaxSizeCounter entityMaxSizeCounter, IGameOverZone gameOverZone,
            IGameplaySaver gameplaySaver, ICoinsModel coinsModel, GameplayFieldVfxHider gameplayFieldVfxHider)
        {
            _entitySpawner = entitySpawner;
            _entitiesRepository = entitiesRepository;
            _entityMaxSizeCounter = entityMaxSizeCounter;
            _scoreCounter = scoreCounter;
            _gameOverZone = gameOverZone;
            _gameplaySaver = gameplaySaver;
            _coinsModel = coinsModel;
            _gameplayFieldVfxHider = gameplayFieldVfxHider;

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
            _gameplayFieldVfxHider.HideGameField();
            _gameOverZone.Reset();
            _scoreCounter.Reset();
            _entityMaxSizeCounter.Reset();
            _entitiesRepository.Reset();
            _entitySpawner.Reset();
            UI_Controller.SetSingleScreens(new[] { ScreenType.Gameplay, ScreenType.BottomMenu });
            
            Initialize();
            _gameplaySaver.Save();
        }
        
        private void ApplySessionResult()
        {
            _coinsModel.AddCoinsByScore(_scoreCounter.Score);
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