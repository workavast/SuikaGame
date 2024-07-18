namespace SuikaGame.Scripts.Coins
{
    public interface ICoinsController
    {
        public int AddCoinsByScore(int score);

        public void ChangeCoinsValue(int changeValue);

        public bool IsCanBuy(int price);
    }
}