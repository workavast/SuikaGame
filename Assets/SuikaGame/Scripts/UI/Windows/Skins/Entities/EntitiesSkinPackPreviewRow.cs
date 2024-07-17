using System;
using SuikaGame.Scripts.Skins;
using SuikaGame.Scripts.Skins.Entities;
using UnityEngine;
using UnityEngine.UI;

namespace SuikaGame.Scripts.UI.Windows.Skins.Entities
{
    [DisallowMultipleComponent]
    [RequireComponent(typeof(Button))]
    public class EntitiesSkinPackPreviewRow : MonoBehaviour
    {
        [SerializeField] private Image preview;
        [SerializeField] private PriceView priceView;

        private EntitiesSkinPackType _entitiesSkinPackType;
        private ISkinsChanger _skinsChanger;

        public event Action<EntitiesSkinPackType> OnClicked;
        
        private void Awake()
        {
            GetComponent<Button>().onClick.AddListener(() => OnClicked?.Invoke(_entitiesSkinPackType));
        }

        public void SetData(ISkinsChanger skinsChanger, EntitiesSkinPackType entitiesSkinPackType, EntitiesSkinPackConfigCell entitiesSkinPackConfigCell)
            => SetData(skinsChanger, entitiesSkinPackType, entitiesSkinPackConfigCell.Preview, entitiesSkinPackConfigCell.Price);
        
        public void SetData(ISkinsChanger skinsChanger, EntitiesSkinPackType entitiesSkinPackType, Sprite newSkinPackPreview, int newPrice)
        {
            if (_skinsChanger != null) 
                _skinsChanger.OnEntitiesSkinPackUnlocked -= TogglePriceVisibility;

            _skinsChanger = skinsChanger;
            _entitiesSkinPackType = entitiesSkinPackType;
            preview.sprite = newSkinPackPreview;
            priceView.SetPrice(newPrice);
            _skinsChanger.OnEntitiesSkinPackUnlocked += TogglePriceVisibility;
            TogglePriceVisibility();
        }
        
        private void TogglePriceVisibility() 
            => priceView.ToggleVisibility(!_skinsChanger.AvailableEntitiesSkinPacks[_entitiesSkinPackType]);
    }
}