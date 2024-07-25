using Avastrad.UI.UI_System;
using SuikaGame.Scripts.Coins;
using SuikaGame.Scripts.Skins;
using SuikaGame.Scripts.Skins.Backgrounds;

namespace SuikaGame.Scripts.UI.Windows.Skins.BuyOrEquiping
{
    public class BuyOrEquiperBackgroundSkin : BuyOrEquiper<BackgroundSkinType>
    {
        private readonly ISkinsChanger _skinsChanger;
        private readonly BackgroundsSkinsConfig _backgroundsSkinsConfig;
        private readonly ICoinsController _coinsModel;
        
        public BuyOrEquiperBackgroundSkin(BuyOrEquipButton buyOrEquipButton, ISkinsChanger skinsChanger, 
            BackgroundsSkinsConfig backgroundsSkinsConfig, ICoinsController coinsModel) 
            : base(buyOrEquipButton)
        {
            _skinsChanger = skinsChanger;
            _backgroundsSkinsConfig = backgroundsSkinsConfig;
            _coinsModel = coinsModel;
        }
        
        public override void BuyOrEquip()
        {
            if (_skinsChanger.AvailableBackgroundSkins[Type])
            {
                _skinsChanger.EquipSkin(Type);
            }
            else
            {
                if (_coinsModel.IsCanBuy(Price))
                {
                    _coinsModel.ChangeCoinsValue(-Price);
                    _skinsChanger.UnlockSkin(Type);
                    _skinsChanger.EquipSkin(Type);
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