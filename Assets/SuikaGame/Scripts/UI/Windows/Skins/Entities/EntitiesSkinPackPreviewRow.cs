using System;
using System.Collections.Generic;
using SuikaGame.Scripts.Skins;
using SuikaGame.Scripts.Skins.Entities;
using SuikaGame.Scripts.UI.Elements;
using UnityEngine;
using UnityEngine.UI;

namespace SuikaGame.Scripts.UI.Windows.Skins.Entities
{
    [DisallowMultipleComponent]
    [RequireComponent(typeof(Button))]
    public class EntitiesSkinPackPreviewRow : MonoBehaviour
    {
        [SerializeField] private PriceView priceView;
        [SerializeField] private Image frame;
        [SerializeField] private Color activeRowColor;
        [SerializeField] private GameObject useMark; 
        
        public EntitiesSkinPackType EntitiesSkinPackType { get; private set; }
        private ISkinsChanger _skinsChanger;
        private Color _unActiveColor;
        private readonly List<ImageHolder> _previews = new();

        public event Action<EntitiesSkinPackType> OnClicked;
        
        private void Awake()
        {
            GetComponent<Button>().onClick.AddListener(() => OnClicked?.Invoke(EntitiesSkinPackType));
            _unActiveColor = frame.color;

            _previews.AddRange(GetComponentsInChildren<ImageHolder>());
        }

        public void SetData(ISkinsChanger skinsChanger, EntitiesSkinPackType entitiesSkinPackType, EntitiesSkinPackConfigCell entitiesSkinPackConfigCell)
            => SetData(skinsChanger, entitiesSkinPackType, entitiesSkinPackConfigCell.Preview, entitiesSkinPackConfigCell.Price);
        
        public void SetData(ISkinsChanger skinsChanger, EntitiesSkinPackType entitiesSkinPackType, IReadOnlyList<Sprite> newSkinPackPreview, int newPrice)
        {
            if (_skinsChanger != null) 
                _skinsChanger.OnEntitiesSkinPackUnlocked -= TogglePriceVisibility;

            _skinsChanger = skinsChanger;
            EntitiesSkinPackType = entitiesSkinPackType;
            
            SetPreviews(newSkinPackPreview);
            priceView.SetPrice(newPrice);
            _skinsChanger.OnEntitiesSkinPackUnlocked += TogglePriceVisibility;
            _skinsChanger.OnEntitiesSkinPackEquipped += UpdateEquipMark;
            TogglePriceVisibility();
            UpdateEquipMark();
        }

        private void SetPreviews(IReadOnlyList<Sprite> newPreviews)
        {
            for (int i = 0; i < newPreviews.Count && i < _previews.Count; i++) 
                _previews[i].sprite = newPreviews[i];
        }
        
        public void ToggleActivity(bool isActive) 
            => frame.color = isActive ? activeRowColor : _unActiveColor;

        private void UpdateEquipMark() 
            => useMark.SetActive(_skinsChanger.EquippedEntitiesSkinPack == EntitiesSkinPackType);

        private void TogglePriceVisibility() 
            => priceView.ToggleVisibility(!_skinsChanger.AvailableEntitiesSkinPacks[EntitiesSkinPackType]);

        private void OnDestroy()
        {
            _skinsChanger.OnEntitiesSkinPackUnlocked -= TogglePriceVisibility;
            _skinsChanger.OnEntitiesSkinPackEquipped -= UpdateEquipMark;        
        }
    }
}