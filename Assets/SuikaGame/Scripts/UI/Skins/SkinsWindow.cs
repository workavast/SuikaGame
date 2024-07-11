using SuikaGame.Scripts.Skins;
using SuikaGame.Scripts.Skins.SkinPackChanging;
using UnityEngine;
using Zenject;

namespace SuikaGame.Scripts.UI.Skins
{
    public class SkinsWindow : MonoBehaviour
    {
        [SerializeField] private SkinPackPreviewView skinPackPreviewView;
        [SerializeField] private SkinsPacksRowsView skinsPacksRowsView;
        
        [Inject] private ISkinPackChanger _skinPackChanger;

        private SkinPackType _currentSkinPackPreview;

        private readonly SkinPackPreviewModel _model = new();
        
        private void Start()
        {
            skinPackPreviewView.Initialize(_model);
            skinsPacksRowsView.Initialize(_model);
            
            Hide();
        }

        public void ApplyPreviewSkin() 
            => _skinPackChanger.ChangeActiveSkinPack(_model.SkinPackPreview);

        public void Show() 
            => gameObject.SetActive(true);

        public void Hide() 
            => gameObject.SetActive(false);
    }
}