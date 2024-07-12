using System;
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

        private EntitiesSkinPackType _entitiesSkinPackType;

        public event Action<EntitiesSkinPackType> OnClicked;
        
        private void Awake()
        {
            GetComponent<Button>().onClick.AddListener(() => OnClicked?.Invoke(_entitiesSkinPackType));
        }

        public void SetData(EntitiesSkinPackType entitiesSkinPackType, EntitiesSkinPackConfigCell entitiesSkinPackConfigCell)
            => SetData(entitiesSkinPackType, entitiesSkinPackConfigCell.Preview);
        
        public void SetData(EntitiesSkinPackType entitiesSkinPackType, Sprite newSkinPackPreview)
        {
            _entitiesSkinPackType = entitiesSkinPackType;
            preview.sprite = newSkinPackPreview;
        }
    }
}