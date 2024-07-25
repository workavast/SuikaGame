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
        [SerializeField] private Image frame;
        [SerializeField] private Color activeRowColor;
        [SerializeField] private GameObject useMark; 

        public BackgroundSkinType BackgroundSkinType { get; private set; }
        private ISkinsChanger _skinsChanger;
        private Color _unActiveColor;

        public event Action<BackgroundSkinType> OnClicked;
        
        private void Awake()
        {
            GetComponent<Button>().onClick.AddListener(() => OnClicked?.Invoke(BackgroundSkinType));
            _unActiveColor = frame.color;
        }

        public void SetData(ISkinsChanger skinsChanger, BackgroundSkinType backgroundSkinType, BackgroundSkinConfigCell backgroundSkinConfigCell)
            => SetData(skinsChanger, backgroundSkinType, backgroundSkinConfigCell.Preview, backgroundSkinConfigCell.Price);
        
        public void SetData(ISkinsChanger skinsChanger, BackgroundSkinType backgroundSkinType, Sprite newBackgroundPreview, int newPrice)
        {
            if (_skinsChanger != null) 
                _skinsChanger.OnBackgroundSkinUnlocked -= TogglePriceVisibility;

            _skinsChanger = skinsChanger;
            BackgroundSkinType = backgroundSkinType;
            preview.sprite = newBackgroundPreview;
            priceView.SetPrice(newPrice);
            _skinsChanger.OnBackgroundSkinUnlocked += TogglePriceVisibility;
            _skinsChanger.OnBackgroundSkinEquipped += UpdateEquipMark;
            TogglePriceVisibility();
            UpdateEquipMark();
        }

        public void ToggleActivity(bool isActive) 
            => frame.color = isActive ? activeRowColor : _unActiveColor;

        private void UpdateEquipMark() 
            => useMark.SetActive(_skinsChanger.EquippedBackgroundSkin == BackgroundSkinType);
        
        private void TogglePriceVisibility() 
            => priceView.ToggleVisibility(!_skinsChanger.AvailableBackgroundSkins[BackgroundSkinType]);
    }
}