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
        [SerializeField] private Image frame;
        [SerializeField] private Color activeRowColor;
        [SerializeField] private GameObject useMark; 
        
        public EntitiesSkinPackType EntitiesSkinPackType { get; private set; }
        private ISkinsChanger _skinsChanger;
        private Color _unActiveColor;

        public event Action<EntitiesSkinPackType> OnClicked;
        
        private void Awake()
        {
            GetComponent<Button>().onClick.AddListener(() => OnClicked?.Invoke(EntitiesSkinPackType));
            _unActiveColor = frame.color;
        }

        public void SetData(ISkinsChanger skinsChanger, EntitiesSkinPackType entitiesSkinPackType, EntitiesSkinPackConfigCell entitiesSkinPackConfigCell)
            => SetData(skinsChanger, entitiesSkinPackType, entitiesSkinPackConfigCell.Preview, entitiesSkinPackConfigCell.Price);
        
        public void SetData(ISkinsChanger skinsChanger, EntitiesSkinPackType entitiesSkinPackType, Sprite newSkinPackPreview, int newPrice)
        {
            if (_skinsChanger != null) 
                _skinsChanger.OnEntitiesSkinPackUnlocked -= TogglePriceVisibility;

            _skinsChanger = skinsChanger;
            EntitiesSkinPackType = entitiesSkinPackType;
            preview.sprite = newSkinPackPreview;
            priceView.SetPrice(newPrice);
            _skinsChanger.OnEntitiesSkinPackUnlocked += TogglePriceVisibility;
            _skinsChanger.OnEntitiesSkinPackEquipped += UpdateEquipMark;
            TogglePriceVisibility();
            UpdateEquipMark();
        }

        public void ToggleActivity(bool isActive) 
            => frame.color = isActive ? activeRowColor : _unActiveColor;

        private void UpdateEquipMark() 
            => useMark.SetActive(_skinsChanger.EquippedEntitiesSkinPack == EntitiesSkinPackType);

        private void TogglePriceVisibility() 
            => priceView.ToggleVisibility(!_skinsChanger.AvailableEntitiesSkinPacks[EntitiesSkinPackType]);
    }
}