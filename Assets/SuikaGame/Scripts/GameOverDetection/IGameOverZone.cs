using System;

namespace SuikaGame.Scripts.GameOverDetection
{
    public interface IGameOverZone
    {
        public bool GameIsOver { get; }

        public event Action OnGameIsOver;
        
        public void Reset();
    }
}