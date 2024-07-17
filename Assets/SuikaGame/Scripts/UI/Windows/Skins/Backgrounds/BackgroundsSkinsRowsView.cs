using System.Collections.Generic;
using System.Linq;
using SuikaGame.Scripts.Coins;
using SuikaGame.Scripts.Skins;
using SuikaGame.Scripts.Skins.Backgrounds;
using UnityEngine;
using Zenject;

namespace SuikaGame.Scripts.UI.Windows.Skins.Backgrounds
{
    public class BackgroundsSkinsRowsView : MonoBehaviour
    {
        [SerializeField] private Transform skinPacksParent;
        [SerializeField] private BackgroundSkinPreviewRow backgroundSkinPreviewRowPrefab;
        [SerializeField] private BuyOrEquipButton buyOrEquipButton;

        private readonly List<BackgroundSkinPreviewRow> _rows = new(4);
        private BackgroundsSkinsConfig _backgroundsSkinsConfig;
        private SkinsPreviewModel _model;
        private ICoinsController _coinsModel;
        private ISkinsChanger _skinsChanger;
            
        [Inject]
        public void Construct(BackgroundsSkinsConfig backgroundsSkinsConfig, ICoinsController coinsModel, 
            ISkinsChanger skinsChanger)
        {
            _backgroundsSkinsConfig = backgroundsSkinsConfig;
            _coinsModel = coinsModel;
            _skinsChanger = skinsChanger;
        }

        public void Initialize(SkinsPreviewModel model)
        {
            _model = model;
            InitializeRows();
            SetRowsData();
            
            UpdateButtonState(_model.BackgroundSkinPreview);
        }

        public void _BuyOrEquip()
        {
            if (_skinsChanger.AvailableBackgroundSkins[_model.BackgroundSkinPreview])
            {
                _skinsChanger.EquipSkin(_model.BackgroundSkinPreview);
            }
            else
            {
                var skinConfigCell = _backgroundsSkinsConfig.BackgroundsSkins[_model.BackgroundSkinPreview];
                if (_coinsModel.IsCanBuy(skinConfigCell.Price))
                {
                    _coinsModel.ChangeCoinsValue(-skinConfigCell.Price);
                    _skinsChanger.UnlockSkin(_model.BackgroundSkinPreview);
                    _skinsChanger.EquipSkin(_model.BackgroundSkinPreview);
                }
            }

            UpdateButtonState(_model.BackgroundSkinPreview);
        }

        private void InitializeRows()
        {
            foreach (var row in _rows)
                row.OnClicked -= ChangeSkinPreview;
            
            var existRows = GetComponentsInChildren<BackgroundSkinPreviewRow>(true).ToList();
            var existRowsCount = existRows.Count;

            var counter = 0;
            for (int i = 0; i < existRowsCount - _backgroundsSkinsConfig.BackgroundsSkins.Count; i++)
            {
                counter++;
                Destroy(existRows[i].gameObject);
            }

            for (int i = counter-1; i >= 0; i--) 
                existRows.RemoveAt(i);

            _rows.Clear();
            _rows.AddRange(existRows);
            for (int i = existRowsCount; i < _backgroundsSkinsConfig.BackgroundsSkins.Count; i++)
            {
                var newRow = Instantiate(backgroundSkinPreviewRowPrefab, skinPacksParent); 
                _rows.Add(newRow);
            }

            foreach (var row in _rows)
                row.OnClicked += ChangeSkinPreview;
        }

        private void ChangeSkinPreview(BackgroundSkinType newSkin)
        {
            _model.ChangeBackgroundPreview(newSkin);
            UpdateButtonState(newSkin);
        }
        
        private void UpdateButtonState(BackgroundSkinType skin)
        {
            if(_skinsChanger.AvailableBackgroundSkins[skin])
                buyOrEquipButton.SetEquipState();
            else
                buyOrEquipButton.SetBuyState(_backgroundsSkinsConfig.BackgroundsSkins[skin].Price);
        }

        private void SetRowsData()
        {
            if(_backgroundsSkinsConfig.BackgroundsSkins.Count != _rows.Count)
                InitializeRows();

            var backgroundSkinTypes = _backgroundsSkinsConfig.BackgroundsSkins.Keys.ToList();
            for (int i = 0; i < _backgroundsSkinsConfig.BackgroundsSkins.Count && i < _rows.Count; i++)
            {
                var key = backgroundSkinTypes[i];
                _rows[i].SetData(_skinsChanger, key, _backgroundsSkinsConfig.BackgroundsSkins[key]);
            }
        }
    }
}