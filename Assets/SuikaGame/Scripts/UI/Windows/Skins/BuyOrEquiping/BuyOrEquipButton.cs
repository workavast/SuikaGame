using SuikaGame.Scripts.Analytics;
using SuikaGame.Scripts.Coins;
using SuikaGame.Scripts.Skins;
using SuikaGame.Scripts.Skins.Backgrounds;
using SuikaGame.Scripts.Skins.Entities;
using TMPro;
using UnityEngine;
using Zenject;

namespace SuikaGame.Scripts.UI.Windows.Skins.BuyOrEquiping
{
    public class BuyOrEquipButton : MonoBehaviour
    {
        [SerializeField] private RowsViewsSwitcher rowsViewsSwitcher;
        [SerializeField] private TMP_Text price;
        [SerializeField] private GameObject buyBlock;
        [SerializeField] private GameObject equipBlock;
        [SerializeField] private GameObject equippedBlock;
        
        private BuyOrEquiperEntitiesSkinPack _buyOrEquiperEntitiesSkinPack;
        private BuyOrEquiperBackgroundSkin _buyOrEquiperBackgroundSkin;

        private IBuyOrEquiper _currBuyOrEquiper;
        private SkinsPreviewModel _model;

        [Inject]
        public void Construct(ISkinsChanger skinsChanger, EntitiesSkinPacksConfig entitiesSkinPacksConfig, 
            BackgroundsSkinsConfig backgroundsSkinsConfig, ICoinsModel coinsModel, IAnalyticsProvider analyticsProvider)
        {
            _buyOrEquiperEntitiesSkinPack = new BuyOrEquiperEntitiesSkinPack(this, skinsChanger, entitiesSkinPacksConfig, coinsModel, analyticsProvider);
            _buyOrEquiperBackgroundSkin = new BuyOrEquiperBackgroundSkin(this, skinsChanger, backgroundsSkinsConfig, coinsModel, analyticsProvider);
        }
        
        public void Initialize(SkinsPreviewModel model)
        {
            _model = model;
            
            rowsViewsSwitcher.OnEntitiesScrollOpen += RowsViewsSwitcherOnOnEntitiesScrollOpen;
            rowsViewsSwitcher.OnBackgroundScrollOpen += RowsViewsSwitcherOnOnBackgroundScrollOpen;
            
            _model.OnEntitiesSkinPackPreviewChanged += UpdateButton;
            _model.OnBackgroundPreviewChanged += UpdateButton;
        }
        
        public void SetBuyState(int newPrice)
        {
            price.text = newPrice.ToString();
            buyBlock.SetActive(true);
            equipBlock.SetActive(false);
            equippedBlock.SetActive(false);
        }

        public void SetEquipState()
        {
            buyBlock.SetActive(false);
            equipBlock.SetActive(true);
            equippedBlock.SetActive(false);
        }
        
        public void SetEquippedState()
        {
            buyBlock.SetActive(false);
            equipBlock.SetActive(false);
            equippedBlock.SetActive(true);
        }
        
        public void _BuyOrEquip() 
            => _currBuyOrEquiper.BuyOrEquip();

        private void RowsViewsSwitcherOnOnBackgroundScrollOpen()
        {
            _currBuyOrEquiper = _buyOrEquiperBackgroundSkin;
            _currBuyOrEquiper.UpdateButtonState();
        }

        private void RowsViewsSwitcherOnOnEntitiesScrollOpen()
        {
            _currBuyOrEquiper = _buyOrEquiperEntitiesSkinPack;
            _currBuyOrEquiper.UpdateButtonState();
        }

        private void UpdateButton(EntitiesSkinPackType skinPackType) 
            => _buyOrEquiperEntitiesSkinPack.Set(skinPackType);

        private void UpdateButton(BackgroundSkinType skinType) 
            => _buyOrEquiperBackgroundSkin.Set(skinType);
    }
}