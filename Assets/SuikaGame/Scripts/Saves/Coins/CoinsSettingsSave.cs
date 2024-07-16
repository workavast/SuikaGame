using System;

namespace SuikaGame.Scripts.Saves.Coins
{
    [Serializable]
    public class CoinsSettingsSave
    {
        public int Coins = 0;

        public CoinsSettingsSave()
        {
            Coins = 0;
        }
        
        public CoinsSettingsSave(CoinsSettings settings)
        {
            Coins = settings.Coins;
        }
    }
}