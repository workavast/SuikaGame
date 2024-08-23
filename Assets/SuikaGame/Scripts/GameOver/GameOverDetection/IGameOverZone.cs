using System;

namespace SuikaGame.Scripts.GameOver.GameOverDetection
{
    public interface IGameOverZone
    {
        public event Action OnGameIsOver;
        
        public void Reset();
    }
}