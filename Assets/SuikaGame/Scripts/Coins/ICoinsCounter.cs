namespace SuikaGame.Scripts.Coins
{
    public interface ICoinsCounter
    {
        public int ReceivedCoins { get; }

        public void Reset();
        public void DoubleCoins();
    }
}