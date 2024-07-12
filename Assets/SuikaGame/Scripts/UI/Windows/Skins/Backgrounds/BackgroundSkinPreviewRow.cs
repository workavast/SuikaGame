using System;
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

        private BackgroundSkinType _backgroundSkinType;

        public event Action<BackgroundSkinType> OnClicked;
        
        private void Awake()
        {
            GetComponent<Button>().onClick.AddListener(() => OnClicked?.Invoke(_backgroundSkinType));
        }

        public void SetData(BackgroundSkinType backgroundSkinType, BackgroundSkinConfigCell backgroundSkinConfigCell)
            => SetData(backgroundSkinType, backgroundSkinConfigCell.Preview);
        
        public void SetData(BackgroundSkinType backgroundSkinType, Sprite newBackgroundPreview)
        {
            _backgroundSkinType = backgroundSkinType;
            preview.sprite = newBackgroundPreview;
        }
    }
}