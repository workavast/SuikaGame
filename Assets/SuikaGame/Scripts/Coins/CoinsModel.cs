using System;
using SuikaGame.Scripts.Saves.Coins;

namespace SuikaGame.Scripts.Coins
{
    public class CoinsModel : ICoinsModel, IDisposable
    {
        private readonly CoinsSettings _coinsSettings;
        private readonly CoinsConfig _coinsConfig;

        public int Coins => _coinsSettings.Coins;
     
        public event Action OnChange;

        public CoinsModel(CoinsSettings coinsModel, CoinsConfig coinsConfig)
        {
            _coinsSettings = coinsModel;
            _coinsConfig = coinsConfig;

            _coinsSettings.OnChange += SendChangeMessage;
        }

        public void AddCoinsByScore(int score) 
            => ChangeCoinsValue(CoinsByScore(score));

        public int CoinsByScore(int score) 
            => (int)(score * _coinsConfig.CoinsPerScore);

        public void ChangeCoinsValue(int changeValue) 
            => _coinsSettings.ChangeCoinsValue(changeValue);

        public bool IsCanBuy(int price) 
            => _coinsSettings.Coins >= price;

        private void SendChangeMessage() 
            => OnChange?.Invoke();

        public void Dispose() 
            => _coinsSettings.OnChange -= SendChangeMessage;
    }
}