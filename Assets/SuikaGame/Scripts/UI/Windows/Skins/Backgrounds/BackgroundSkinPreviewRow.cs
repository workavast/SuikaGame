using System;
using SuikaGame.Scripts.Skins;
using SuikaGame.Scripts.Skins.Backgrounds;
using UnityEngine;
using UnityEngine.UI;

namespace SuikaGame.Scripts.UI.Windows.Skins.Backgrounds
{
    [DisallowMultipleComponent]
    [RequireComponent(typeof(Button))]
    public class BackgroundSkinPreviewRow : MonoBehaviour
    {
        [SerializeField] private Image preview;
        [SerializeField] private PriceView priceView;

        private BackgroundSkinType _backgroundSkinType;
        private ISkinsChanger _skinsChanger;
        
        public event Action<BackgroundSkinType> OnClicked;
        
        private void Awake()
        {
            GetComponent<Button>().onClick.AddListener(() => OnClicked?.Invoke(_backgroundSkinType));
        }

        public void SetData(ISkinsChanger skinsChanger, BackgroundSkinType backgroundSkinType, BackgroundSkinConfigCell backgroundSkinConfigCell)
            => SetData(skinsChanger, backgroundSkinType, backgroundSkinConfigCell.Preview, backgroundSkinConfigCell.Price);
        
        public void SetData(ISkinsChanger skinsChanger, BackgroundSkinType backgroundSkinType, Sprite newBackgroundPreview, int newPrice)
        {
            if (_skinsChanger != null) 
                _skinsChanger.OnBackgroundSkinUnlocked -= TogglePriceVisibility;

            _skinsChanger = skinsChanger;
            _backgroundSkinType = backgroundSkinType;
            preview.sprite = newBackgroundPreview;
            priceView.SetPrice(newPrice);
            _skinsChanger.OnBackgroundSkinUnlocked += TogglePriceVisibility;
            TogglePriceVisibility();
        }

        private void TogglePriceVisibility() 
            => priceView.ToggleVisibility(!_skinsChanger.AvailableBackgroundSkins[_backgroundSkinType]);
    }
}