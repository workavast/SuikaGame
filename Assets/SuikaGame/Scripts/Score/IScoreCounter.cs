using System;

namespace SuikaGame.Scripts.Score
{
    public interface IScoreCounter
    {
        public int Score { get; }

        /// <summary>
        /// return current score value
        /// </summary>
        public event Action<int> OnScoreChanged;

        public void Reset();
    }
}