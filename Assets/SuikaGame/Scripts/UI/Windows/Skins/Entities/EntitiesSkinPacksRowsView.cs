using System;
using System.Collections.Generic;
using System.Linq;
using SuikaGame.Scripts.Coins;
using SuikaGame.Scripts.Skins;
using SuikaGame.Scripts.Skins.Entities;
using UnityEngine;
using Zenject;

namespace SuikaGame.Scripts.UI.Windows.Skins.Entities
{
    public class EntitiesSkinPacksRowsView : MonoBehaviour
    {
        [SerializeField] private Transform skinsPacksParent;
        [SerializeField] private EntitiesSkinPackPreviewRow entitiesSkinPackPreviewRowPrefab;
        [SerializeField] private BuyOrEquipButton buyOrEquipButton;

        private readonly List<EntitiesSkinPackPreviewRow> _rows = new(8);
        private EntitiesSkinPacksConfig _entitiesSkinPacksConfig;
        private SkinsPreviewModel _model;
        private ICoinsController _coinsModel;
        private ISkinsChanger _skinsChanger;
        
        [Inject]
        public void Construct(EntitiesSkinPacksConfig entitiesSkinPacksConfig, ICoinsController coinsModel, 
            ISkinsChanger skinsChanger)
        {
            _entitiesSkinPacksConfig = entitiesSkinPacksConfig;
            _coinsModel = coinsModel;
            _skinsChanger = skinsChanger;
        }
        
        public void Initialize(SkinsPreviewModel model)
        {
            _model = model;
            InitializeRows();
            SetRowsData();

            UpdateButtonState(_model.EntitiesSkinPackPreview);
        }
        
        public void _BuyOrEquip()
        {
            if (_skinsChanger.AvailableEntitiesSkinPacks[_model.EntitiesSkinPackPreview])
            {
                _skinsChanger.EquipSkin(_model.EntitiesSkinPackPreview);
            }
            else
            {
                var skinConfigCell = _entitiesSkinPacksConfig.SkinsPacks[_model.EntitiesSkinPackPreview];
                if (_coinsModel.IsCanBuy(skinConfigCell.Price))
                {
                    _coinsModel.ChangeCoinsValue(-skinConfigCell.Price);
                    _skinsChanger.UnlockSkin(_model.EntitiesSkinPackPreview);
                    _skinsChanger.EquipSkin(_model.EntitiesSkinPackPreview);
                }
            }
            
            UpdateButtonState(_model.EntitiesSkinPackPreview);
        }
        
        private void InitializeRows()
        {
            foreach (var row in _rows)
                row.OnClicked -= ChangeSkinPackPreview;
            
            var existRows = GetComponentsInChildren<EntitiesSkinPackPreviewRow>(true).ToList();
            int existRowsCount = existRows.Count;

            var counter = 0;
            for (int i = 0; i < existRowsCount - _entitiesSkinPacksConfig.SkinsPacks.Count; i++)
            {
                counter++;
                Destroy(existRows[i].gameObject);
            }

            for (int i = counter-1; i >= 0; i--) 
                existRows.RemoveAt(i);

            _rows.Clear();
            _rows.AddRange(existRows);
            for (int i = existRowsCount; i < _entitiesSkinPacksConfig.SkinsPacks.Count; i++)
            {
                var newRow = Instantiate(entitiesSkinPackPreviewRowPrefab, skinsPacksParent); 
                _rows.Add(newRow);
            }

            foreach (var row in _rows)
                row.OnClicked += ChangeSkinPackPreview;
        }

        private void ChangeSkinPackPreview(EntitiesSkinPackType newEntitiesSkinPack)
        {
            _model.ChangeEntityPreview(newEntitiesSkinPack);
            UpdateButtonState(newEntitiesSkinPack);
        }

        private void UpdateButtonState(EntitiesSkinPackType entitiesSkinPack)
        {
            if(_skinsChanger.AvailableEntitiesSkinPacks[entitiesSkinPack])
                buyOrEquipButton.SetEquipState();
            else
                buyOrEquipButton.SetBuyState(_entitiesSkinPacksConfig.SkinsPacks[entitiesSkinPack].Price);
        }

        private void SetRowsData()
        {
            if(_entitiesSkinPacksConfig.SkinsPacks.Count != _rows.Count)
                InitializeRows();

            var skinsPacks = _entitiesSkinPacksConfig.SkinsPacks.Keys.ToList();
            for (int i = 0; i < _entitiesSkinPacksConfig.SkinsPacks.Count && i < _rows.Count; i++)
            {
                var key = skinsPacks[i];
                _rows[i].SetData(_skinsChanger, key, _entitiesSkinPacksConfig.SkinsPacks[key]);
            }
        }
    }
}