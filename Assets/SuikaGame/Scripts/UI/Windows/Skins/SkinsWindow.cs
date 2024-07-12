using SuikaGame.Scripts.Skins.Entities;
using SuikaGame.Scripts.Skins.SkinsChanging;
using SuikaGame.Scripts.UI.Windows.Skins.Backgrounds;
using SuikaGame.Scripts.UI.Windows.Skins.Entities;
using SuikaGame.Scripts.UI.Windows.Skins.Preview;
using UnityEngine;
using Zenject;

namespace SuikaGame.Scripts.UI.Windows.Skins
{
    public class SkinsWindow : MonoBehaviour
    {
        [SerializeField] private SkinPackPreviewView skinPackPreviewView;
        [SerializeField] private EntitiesSkinPacksRowsView entitiesSkinPacksRowsView;
        [SerializeField] private BackgroundsSkinsRowsView backgroundsSkinsRowsView;
        
        [Inject] private ISkinsChanger _skinsChanger;

        private EntitiesSkinPackType _currentEntitiesSkinPackPreview;

        private readonly SkinsPreviewModel _model = new();
        
        private void Start()
        {
            skinPackPreviewView.Initialize(_model);
            entitiesSkinPacksRowsView.Initialize(_model);
            backgroundsSkinsRowsView.Initialize(_model);
            
            Hide();
        }

        public void ApplyPreviewSkins()
        {
            _skinsChanger.ChangeActiveEntitySkin(_model.EntitiesSkinPackPreview);
            _skinsChanger.ChangeActiveBackgroundSkin(_model.BackgroundSkinPreview);
        }

        public void Show() 
            => gameObject.SetActive(true);

        public void Hide() 
            => gameObject.SetActive(false);
    }
}