namespace SuikaGame.Scripts.Coins
{
    public interface ICoinsChangeModel
    {
        public void ChangeCoinsValue(int changeValue);
        
        public void AddCoinsByScore(int score);
    }
}