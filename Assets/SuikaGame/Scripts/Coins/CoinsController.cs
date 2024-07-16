namespace SuikaGame.Scripts.Coins
{
    public class CoinsController : ICoinsController
    {
        private readonly ICoinsModel _coinsSettings;
        private readonly CoinsConfig _coinsConfig;

        public CoinsController(ICoinsModel coinsModel, CoinsConfig coinsConfig)
        {
            _coinsSettings = coinsModel;
            _coinsConfig = coinsConfig;
        }

        public void AddCoinsByScore(int score)
        {
            var coins = (int)(score * _coinsConfig.CoinsPerScore);
            ChangeCoinsValue(coins);
        }
        
        public void ChangeCoinsValue(int changeValue) 
            => _coinsSettings.ChangeCoinsValue(changeValue);

        public bool IsCanBuy(int price) 
            => _coinsSettings.Coins >= price;
    }
}