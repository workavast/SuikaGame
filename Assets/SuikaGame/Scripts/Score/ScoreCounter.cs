using System;
using Avastrad.EventBusFramework;

namespace SuikaGame.Scripts.Score
{
    public class ScoreCounter : IScoreCounter, IDisposable, IEventReceiver<EntityCollisionEvent>
    {
        public EventBusReceiverIdentifier EventBusReceiverIdentifier { get; } = new();
        private readonly IEventBus _eventBus;
        private readonly ScoreConfig _scoreConfig;

        public int Score { get; private set; }
        public event Action<int> OnScoreChanged;

        public ScoreCounter(IEventBus eventBus, ScoreConfig scoreConfig)
        {
            _eventBus = eventBus;
            _scoreConfig = scoreConfig;
            
            _eventBus.Subscribe(this);
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