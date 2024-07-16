using SuikaGame.Scripts.Skins;
using SuikaGame.Scripts.Skins.Entities;
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
        [SerializeField] private RowsViewsSwitcher rowsViewsSwitcher;
        
        private ISkinsChanger _skinsChanger;
        private EntitiesSkinPackType _currentEntitiesSkinPackPreview;
        private readonly SkinsPreviewModel _model = new();

        [Inject]
        public void Construct(ISkinsChanger skinsChanger)
        {
            _skinsChanger = skinsChanger;
        }
        
        private void Start()
        {
            _model.ChangeEntityPreview(_skinsChanger.ActiveEntitiesSkinPack);
            _model.ChangeBackgroundPreview(_skinsChanger.ActiveBackgroundSkin);
            
            skinPackPreviewView.Initialize(_model);
            entitiesSkinPacksRowsView.Initialize(_model);
            backgroundsSkinsRowsView.Initialize(_model);
            rowsViewsSwitcher.Initialize();
            
            Hide();
        }

        public void ApplyPreviewSkins()
        {
            _skinsChanger.SetEntitiesSkinPack(_model.EntitiesSkinPackPreview);
            _skinsChanger.SetBackgroundSkin(_model.BackgroundSkinPreview);
        }

        public void Show() 
            => gameObject.SetActive(true);

        public void Hide() 
            => gameObject.SetActive(false);
    }
}