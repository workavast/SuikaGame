using System;
using Avastrad.EventBusFramework;

namespace SuikaGame.Scripts.Coins
{
    public class CoinsCounter : ICoinsCounter, IEventReceiver<MergeEvent>, IDisposable
    {
        public int ReceivedCoins { get; private set; }
        public EventBusReceiverIdentifier EventBusReceiverIdentifier { get; } = new();
        
        private readonly ICoinsModel _coinsModel;
        private readonly IEventBus _eventBus;
        private readonly CoinsConfig _config;

        public CoinsCounter(ICoinsModel coinsModel, CoinsConfig config, IEventBus eventBus)
        {
            _coinsModel = coinsModel;
            _eventBus = eventBus;
            _config = config;

            _eventBus.Subscribe(this);
        }

        public void Reset() 
            => ReceivedCoins = 0;

        public void DoubleCoins() 
            => _coinsModel.ChangeCoinsValue(ReceivedCoins);

        public void OnEvent(MergeEvent t)
        {
            var coins = _config.CoinsPerMerge[t.SizeIndex];
            ReceivedCoins += coins;
            _coinsModel.ChangeCoinsValue(coins);
        }

        public void Dispose()
        {
            _eventBus?.UnSubscribe(this);
        }
    }
}