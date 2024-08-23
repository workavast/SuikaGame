namespace SuikaGame.Scripts.Coins
{
    public interface ICoinsChangeModel
    {
        public void ChangeCoinsValue(int changeValue, bool save = false);
        
        public void AddCoinsByScore(int score, bool save = false);
    }
}