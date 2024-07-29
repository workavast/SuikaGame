using Avastrad.UI.UI_System;
using SuikaGame.Scripts.Game;
using SuikaGame.Scripts.Skins;
using SuikaGame.Scripts.Skins.Entities;
using SuikaGame.Scripts.UI.Elements.AnimationBlocks;
using SuikaGame.Scripts.UI.Windows.Skins.Backgrounds;
using SuikaGame.Scripts.UI.Windows.Skins.BuyOrEquiping;
using SuikaGame.Scripts.UI.Windows.Skins.Entities;
using SuikaGame.Scripts.UI.Windows.Skins.Preview;
using UnityEngine;
using Zenject;

namespace SuikaGame.Scripts.UI.Windows.Skins
{
    public class SkinsWindow : UI_ScreenBase
    {
        [SerializeField] private AnimationFadeBlock backgroundFadeBlock;
        [SerializeField] private AnimationFadeBlock previewFadeBlock;
        [SerializeField] private AnimationMoveBlock scrollsMoveBlock;
        [SerializeField] private AnimationMoveBlock upTitleMoveBlock;

        [Space]
        [SerializeField] private SkinPackPreviewView skinPackPreviewView;
        [SerializeField] private BackgroundPreviewView backgroundPreviewView;
        [SerializeField] private EntitiesSkinPacksRowsView entitiesSkinPacksRowsView;
        [SerializeField] private BackgroundsSkinsRowsView backgroundsSkinsRowsView;
        [SerializeField] private RowsViewsSwitcher rowsViewsSwitcher;
        [SerializeField] private BuyOrEquipButton buyOrEquipButton;
        
        private ISkinsChanger _skinsChanger;
        private EntitiesSkinPackType _currentEntitiesSkinPackPreview;
        private readonly SkinsPreviewModel _model = new();

        [Inject]
        public void Construct(ISkinsChanger skinsChanger)
        {
            _skinsChanger = skinsChanger;
        }
        
        public override void Initialize()
        {
            _model.ChangeEntityPreview(_skinsChanger.EquippedEntitiesSkinPack);
            _model.ChangeBackgroundPreview(_skinsChanger.EquippedBackgroundSkin);
            
            skinPackPreviewView.Initialize(_model);
            backgroundPreviewView.Initialize(_model);
            entitiesSkinPacksRowsView.Initialize(_model);
            backgroundsSkinsRowsView.Initialize(_model);
            buyOrEquipButton.Initialize(_model);
            rowsViewsSwitcher.Initialize();
            
            Hide();
        }
        
        public override void Show()
        {
            GamePauser.Pause();
            gameObject.SetActive(true);
            
            backgroundFadeBlock.Show();
            previewFadeBlock.Show();
            scrollsMoveBlock.Show();
            upTitleMoveBlock.Show();
        }

        public override void Hide()
        {
            backgroundFadeBlock.Hide();
            previewFadeBlock.Hide();
            scrollsMoveBlock.Hide(() =>
            {
                gameObject.SetActive(false);
                GamePauser.Continue();
            });
            upTitleMoveBlock.Hide();
        }
    }
}