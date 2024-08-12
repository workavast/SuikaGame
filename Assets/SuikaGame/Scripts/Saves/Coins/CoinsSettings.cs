using System;

namespace SuikaGame.Scripts.Saves.Coins
{
    public class CoinsSettings : ISettings
    {
        public int Coins { get; private set; }
        
        public event Action OnChange;
        
        public CoinsSettings()
        {
            Coins = 0;
        }

        public void ChangeCoinsValue(int changeValue)
        {
            if (changeValue == 0)
                return;
            
            Coins += changeValue;
        }

        public void Apply()
            => OnChange?.Invoke();

        public void LoadData(CoinsSettingsSave save)
        {
            Coins = save.Coins;
        }
    }
}