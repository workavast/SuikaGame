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
        [SerializeField] private BackgroundPreviewView backgroundPreviewView;
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
            _model.ChangeEntityPreview(_skinsChanger.EquippedEntitiesSkinPack);
            _model.ChangeBackgroundPreview(_skinsChanger.EquippedBackgroundSkin);
            
            skinPackPreviewView.Initialize(_model);
            backgroundPreviewView.Initialize(_model);
            entitiesSkinPacksRowsView.Initialize(_model);
            backgroundsSkinsRowsView.Initialize(_model);
            rowsViewsSwitcher.Initialize();
            
            Hide();
        }

        public void Show() 
            => gameObject.SetActive(true);

        public void Hide() 
            => gameObject.SetActive(false);
    }
}