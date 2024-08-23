using System;
using SuikaGame.Scripts.Saves;
using SuikaGame.Scripts.Saves.Coins;

namespace SuikaGame.Scripts.Coins
{
    public class CoinsModel : ICoinsModel
    {
        private readonly CoinsSettings _coinsSettings;
        private readonly CoinsConfig _coinsConfig;

        public int Coins => _coinsSettings.Coins;
     
        public event Action OnChange;

        public CoinsModel(CoinsSettings coinsModel, CoinsConfig coinsConfig)
        {
            _coinsSettings = coinsModel;
            _coinsConfig = coinsConfig;
        }

        public void AddCoinsByScore(int score, bool save = false) 
            => ChangeCoinsValue(CoinsByScore(score), save);

        public int CoinsByScore(int score) 
            => (int)(score * _coinsConfig.CoinsPerScore);

        public void ChangeCoinsValue(int changeValue, bool save = false)
        {
            if (changeValue == 0)
                return;

            _coinsSettings.ChangeCoinsValue(changeValue);
            if (save)
                PlayerData.Instance.SaveData();
            OnChange?.Invoke();
        }

        public bool IsCanBuy(int price) 
            => _coinsSettings.Coins >= price;
    }
}