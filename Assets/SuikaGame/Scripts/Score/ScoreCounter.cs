using System;
using Avastrad.EventBusFramework;
using SuikaGame.Scripts.GameOverDetection;
using SuikaGame.Scripts.GameplayField;
using SuikaGame.Scripts.GameplayField.Model;
using SuikaGame.Scripts.Saves.Score;

namespace SuikaGame.Scripts.Score
{
    public class ScoreCounter : IScoreCounter, IDisposable, IEventReceiver<MergeEvent>
    {
        public EventBusReceiverIdentifier EventBusReceiverIdentifier { get; } = new();
        private readonly IEventBus _eventBus;
        private readonly ScoreConfig _scoreConfig;
        private readonly ScoreSettings _scoreSettings;
        private readonly IGameOverZone _gameOverZone;

        public bool IsNewRecord { get; private set; }
        public int Score { get; private set; }
        public int Record => _scoreSettings.ScoreRecord;
        public event Action<int> OnScoreChanged;
        public event Action<int> OnRecordChanged;

        public ScoreCounter(IEventBus eventBus, ScoreConfig scoreConfig, ScoreSettings scoreSettings, 
            IGameplayFieldReadModel gameplayFieldReadModel, IGameOverZone gameOverZone)
        {
            _eventBus = eventBus;
            _scoreConfig = scoreConfig;
            _scoreSettings = scoreSettings;
            _gameOverZone = gameOverZone;

            Score = gameplayFieldReadModel.Score;
            _eventBus.Subscribe(this);
        }

        public void ApplyRecord()
        {
             if (Score > Record)
             {
                 IsNewRecord = true;
                 _scoreSettings.SetScoreRecord(Score);
                 OnRecordChanged?.Invoke(Record);
             }
        }

        public void Reset()
        {
            IsNewRecord = false;
            Score = 0;
            OnScoreChanged?.Invoke(Score);        
        }
        
        public void OnEvent(MergeEvent t)
        {
            if (_gameOverZone.GameIsOver)
                return;

            Score += _scoreConfig.GetScore(t.SizeIndex);
            OnScoreChanged?.Invoke(Score);
        }

        public void Dispose()
        {
            _eventBus.UnSubscribe(this);
        }
    }
}