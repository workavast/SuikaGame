using SuikaGame.Scripts.Entities;
using SuikaGame.Scripts.Entities.Spawning;
using SuikaGame.Scripts.EntityMaxSizeCounting;
using SuikaGame.Scripts.GameOverDetection;
using SuikaGame.Scripts.GameplaySavers;
using SuikaGame.Scripts.Score;
using SuikaGame.Scripts.UI;
using UnityEngine;
using Zenject;

namespace SuikaGame.Scripts.GameControlling
{
    public class GameController : MonoBehaviour, IGameReseter
    {
        [SerializeField] private EndScreen endScreen;
        
        private IEntitySpawner _entitySpawner;
        private IEntitiesRepository _entitiesRepository;
        private IScoreCounter _scoreCounter;
        private IEntityMaxSizeCounter _entityMaxSizeCounter;
        private IGameOverZone _gameOverZone;
        private IGameplaySaver _gameplaySaver;
        
        [Inject]
        public void Construct(
            IEntitySpawner entitySpawner, 
            IEntitiesRepository entitiesRepository, IScoreCounter scoreCounter, 
            IEntityMaxSizeCounter entityMaxSizeCounter, IGameOverZone gameOverZone,
             IGameplaySaver gameplaySaver)
        {
            _entitySpawner = entitySpawner;
            _entitiesRepository = entitiesRepository;
            _entityMaxSizeCounter = entityMaxSizeCounter;
            _scoreCounter = scoreCounter;
            _gameOverZone = gameOverZone;
            _gameplaySaver = gameplaySaver;
        }

        public void ResetGame()
        {
            Debug.Log($"RESET");
            _gameOverZone.Reset();
            _scoreCounter.Reset();
            _entityMaxSizeCounter.Reset();
            _entitiesRepository.Reset();
            _entitySpawner.Reset();
            endScreen.Hide();
            
            _gameplaySaver.Save();
        }
    }
}