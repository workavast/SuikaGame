using System;
using SuikaGame.Scripts.Skins;
using UnityEngine;
using UnityEngine.UI;

namespace SuikaGame.Scripts.UI.Skins
{
    [DisallowMultipleComponent]
    [RequireComponent(typeof(Button))]
    public class SkinPackRow : MonoBehaviour
    {
        [SerializeField] private Image background;
        [SerializeField] private Image ballsPreview;

        private SkinPackType _skinPackType;

        public event Action<SkinPackType> OnClicked;
        
        private void Awake()
        {
            GetComponent<Button>().onClick.AddListener(() => OnClicked?.Invoke(_skinPackType));
        }

        public void SetData(SkinPackType skinPack, SkinPackConfigCell skinPackConfigCell)
            => SetData(skinPack, skinPackConfigCell.BackgroundPreview, skinPackConfigCell.EntitiesPreview);
        
        public void SetData(SkinPackType skinPack, Sprite newBackground, Sprite newBallsPreview)
        {
            _skinPackType = skinPack;
            background.sprite = newBackground;
            ballsPreview.sprite = newBallsPreview;
        }
    }
}