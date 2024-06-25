using SuikaGame.Scripts.Entities;
using SuikaGame.Scripts.Entities.Spawning;
using SuikaGame.Scripts.EntityMaxSizeCounting;
using SuikaGame.Scripts.GameOverDetection;
using SuikaGame.Scripts.Score;
using UnityEngine;
using Zenject;

namespace SuikaGame.Scripts.GameControlling
{
    public class GameController : MonoBehaviour, IGameReseter
    {
        [SerializeField] private GameOverZone _gameOverZone;
        
        private IEntitySpawner _entitySpawner;
        private IEntitiesRepository _entitiesRepository;
        private IScoreCounter _scoreCounter;
        private IEntityMaxSizeCounter _entityMaxSizeCounter;
        
        [Inject]
        public void Construct(
            IEntitySpawner entitySpawner, 
            IEntitiesRepository entitiesRepository, IScoreCounter scoreCounter, 
            IEntityMaxSizeCounter entityMaxSizeCounter)
        {
            _entitySpawner = entitySpawner;
            _entitiesRepository = entitiesRepository;
            _entityMaxSizeCounter = entityMaxSizeCounter;
            _scoreCounter = scoreCounter;
        }

        private void Awake()
        {
            _gameOverZone.OnGameOver += ResetGame;
        }

        public void ResetGame()
        {
            Debug.Log($"RESET");
            _scoreCounter.Reset();
            _entityMaxSizeCounter.Reset();
            _entitiesRepository.Reset();
            _entitySpawner.Reset();
        }
    }
}