using System;
using Avastrad.EventBusFramework;
using SuikaGame.Scripts.GameplayField;
using SuikaGame.Scripts.Saves.Score;

namespace SuikaGame.Scripts.Score
{
    public class ScoreCounter : IScoreCounter, IDisposable, IEventReceiver<EntityCollisionEvent>
    {
        public EventBusReceiverIdentifier EventBusReceiverIdentifier { get; } = new();
        private readonly IEventBus _eventBus;
        private readonly ScoreConfig _scoreConfig;
        private readonly ScoreSettings _scoreSettings;

        public int Score { get; private set; }
        public int Record => _scoreSettings.ScoreRecord;
        public event Action<int> OnScoreChanged;
        public event Action<int> OnRecordChanged;

        public ScoreCounter(IEventBus eventBus, ScoreConfig scoreConfig, ScoreSettings scoreSettings, 
            IGameplayFieldReadModel gameplayFieldReadModel)
        {
            _eventBus = eventBus;
            _scoreConfig = scoreConfig;
            _scoreSettings = scoreSettings;
            
            Score = gameplayFieldReadModel.Score;
            _eventBus.Subscribe(this);
        }

        public void ApplyRecord()
        {
             if (Score > Record)
             {
                 _scoreSettings.SetScoreRecord(Score);
                 OnRecordChanged?.Invoke(Record);
             }
        }

        public void Reset()
        {
            Score = 0;
            OnScoreChanged?.Invoke(Score);        
        }
        
        public void OnEvent(EntityCollisionEvent t)
        {
            Score += _scoreConfig.GetScore(t.Parent.SizeIndex);
            OnScoreChanged?.Invoke(Score);
        }

        public void Dispose()
        {
            _eventBus.UnSubscribe(this);
        }
    }
}