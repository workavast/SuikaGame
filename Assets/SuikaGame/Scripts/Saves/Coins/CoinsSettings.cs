using System;

namespace SuikaGame.Scripts.Saves.Coins
{
    public class CoinsSettings : ISettings
    {
        public bool IsChanged { get; private set; }
        
        public int Coins { get; private set; }
        
        public CoinsSettings()
        {
            Coins = 0;
        }

        public void ChangeCoinsValue(int changeValue)
        {
            if (changeValue == 0)
                return;

            IsChanged = true;
            Coins += changeValue;
        }

        public void LoadData(CoinsSettingsSave save)
        {
            Coins = save.Coins;
        }
        
        public void ResetChangedMarker() 
            => IsChanged = false;
    }
}