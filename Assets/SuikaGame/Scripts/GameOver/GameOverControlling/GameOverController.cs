using System;
using SuikaGame.Scripts.GameOver.GameOverDetection;
using UnityEngine;

namespace SuikaGame.Scripts.GameOver.GameOverControlling
{
    public class GameOverController : IGameOverController
    {
        public bool GameIsOver { get; private set; }
        
        private readonly IGameOverZone _gameOverZone;
        
        public event Action OnGameIsOvered;

        public GameOverController(IGameOverZone gameOverZone)
        {
            _gameOverZone = gameOverZone;
            _gameOverZone.OnGameIsOver += OnGameIsOver;
        }

        public void ManualGameOver() 
            => OnGameIsOver();

        private void OnGameIsOver()
        {
            if (GameIsOver)
            {
                Debug.LogWarning("Game already overed, but you try invoke game over");
                return;
            }
            
            GameIsOver = true;
            OnGameIsOvered?.Invoke();
        }

        public void Reset()
        {
            GameIsOver = false;
            _gameOverZone.Reset();
        }
    }
}