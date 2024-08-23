using System;

namespace SuikaGame.Scripts.GameOver.GameOverControlling
{
    public interface IGameOverProvider
    {
        public bool GameIsOver { get; }

        public event Action OnGameIsOvered;
    }
}