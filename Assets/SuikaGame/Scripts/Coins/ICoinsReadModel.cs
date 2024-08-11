using System;

namespace SuikaGame.Scripts.Coins
{
    public interface ICoinsReadModel
    {
        public event Action OnChange;
        
        public int Coins { get; }
        
        public bool IsCanBuy(int price);
        
        public int CoinsByScore(int score);
    }
}