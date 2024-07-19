namespace SuikaGame.Scripts.Coins
{
    public interface ICoinsController
    {
        public void AddCoinsByScore(int score);

        public int ScoreToCoins(int score);
        
        public void ChangeCoinsValue(int changeValue);

        public bool IsCanBuy(int price);
    }
}