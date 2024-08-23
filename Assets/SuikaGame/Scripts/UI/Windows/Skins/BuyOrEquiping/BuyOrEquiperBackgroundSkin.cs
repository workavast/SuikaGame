using Avastrad.UI.UI_System;
using SuikaGame.Scripts.Analytics;
using SuikaGame.Scripts.Coins;
using SuikaGame.Scripts.Saves;
using SuikaGame.Scripts.Skins;
using SuikaGame.Scripts.Skins.Backgrounds;

namespace SuikaGame.Scripts.UI.Windows.Skins.BuyOrEquiping
{
    public class BuyOrEquiperBackgroundSkin : BuyOrEquiper<BackgroundSkinType>
    {
        private readonly ISkinsChanger _skinsChanger;
        private readonly BackgroundsSkinsConfig _backgroundsSkinsConfig;
        private readonly ICoinsModel _coinsModel;
        private readonly IAnalyticsProvider _analyticsProvider;

        public BuyOrEquiperBackgroundSkin(BuyOrEquipButton buyOrEquipButton, ISkinsChanger skinsChanger, 
            BackgroundsSkinsConfig backgroundsSkinsConfig, ICoinsModel coinsModel, IAnalyticsProvider analyticsProvider)
            : base(buyOrEquipButton)
        {
            _skinsChanger = skinsChanger;
            _backgroundsSkinsConfig = backgroundsSkinsConfig;
            _coinsModel = coinsModel;
            _analyticsProvider = analyticsProvider;
        }
        
        public override void BuyOrEquip()
        {
            if (_skinsChanger.AvailableBackgroundSkins[Type])
            {
                _skinsChanger.EquipSkin(Type);
                PlayerData.Instance.SaveData();
            }
            else
            {
                if (_coinsModel.IsCanBuy(Price))
                {
                    _coinsModel.ChangeCoinsValue(-Price);
                    _skinsChanger.UnlockSkin(Type);
                    _analyticsProvider.SendEvent(AnalyticsKeys.BackgroundSkins[Type]);
                    _skinsChanger.EquipSkin(Type);
                    PlayerData.Instance.SaveData();
                }
                else
                {
                    UI_Controller.ToggleScreen(ScreenType.RewardedAd, true);
                }
            }

            UpdateButtonState();
        }

        public override void UpdateButtonState()
        {
            if (_skinsChanger.EquippedBackgroundSkin == Type)
                BuyOrEquipButton.SetEquippedState();
            else if(_skinsChanger.AvailableBackgroundSkins[Type])
                BuyOrEquipButton.SetEquipState();
            else
                BuyOrEquipButton.SetBuyState(_backgroundsSkinsConfig.BackgroundsSkins[Type].Price);
        }
        
        protected override int GetPrice() 
            => _backgroundsSkinsConfig.BackgroundsSkins[Type].Price;
    }
}