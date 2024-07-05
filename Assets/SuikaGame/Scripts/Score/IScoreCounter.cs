using System;

namespace SuikaGame.Scripts.Score
{
    public interface IScoreCounter
    {
        public int Score { get; }
        public int Record { get; }

        /// <summary>
        /// return current score value
        /// </summary>
        public event Action<int> OnScoreChanged;
        /// <summary>
        /// return current record value
        /// </summary>
        public event Action<int> OnRecordChanged;
        
        public void ApplyRecord();
        public void Reset();
    }
}