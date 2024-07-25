using Avastrad.UI.UI_System;
using SuikaGame.Scripts.Coins;
using SuikaGame.Scripts.Skins;
using SuikaGame.Scripts.Skins.Entities;

namespace SuikaGame.Scripts.UI.Windows.Skins.BuyOrEquiping
{
    public class BuyOrEquiperEntitiesSkinPack : BuyOrEquiper<EntitiesSkinPackType>
    {
        private readonly ISkinsChanger _skinsChanger;
        private readonly EntitiesSkinPacksConfig _entitiesSkinPacksConfig;
        private readonly ICoinsController _coinsModel;
         
        public BuyOrEquiperEntitiesSkinPack(BuyOrEquipButton buyOrEquipButton, ISkinsChanger skinsChanger,
            EntitiesSkinPacksConfig entitiesSkinPacksConfig, ICoinsController coinsModel) 
            : base(buyOrEquipButton)
        {
            _skinsChanger = skinsChanger;
            _entitiesSkinPacksConfig = entitiesSkinPacksConfig;
            _coinsModel = coinsModel;
        }
        
        public override void BuyOrEquip()
        {
            if (_skinsChanger.AvailableEntitiesSkinPacks[Type])
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
            if (_skinsChanger.EquippedEntitiesSkinPack == Type)
                BuyOrEquipButton.SetEquippedState();
            else if(_skinsChanger.AvailableEntitiesSkinPacks[Type])
                BuyOrEquipButton.SetEquipState();
            else
                BuyOrEquipButton.SetBuyState(_entitiesSkinPacksConfig.SkinsPacks[Type].Price);
        }
        
        protected override int GetPrice() 
            => _entitiesSkinPacksConfig.SkinsPacks[Type].Price;
    }
}