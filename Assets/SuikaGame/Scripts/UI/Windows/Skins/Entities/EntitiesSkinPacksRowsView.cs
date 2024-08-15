using System.Collections.Generic;
using System.Linq;
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

        private readonly List<EntitiesSkinPackPreviewRow> _rows = new(8);
        private EntitiesSkinPacksConfig _entitiesSkinPacksConfig;
        private SkinsPreviewModel _model;
        private ISkinsChanger _skinsChanger;
        private EntitiesSkinPackPreviewRow _activeRow;
        
        [Inject]
        public void Construct(EntitiesSkinPacksConfig entitiesSkinPacksConfig, ISkinsChanger skinsChanger)
        {
            _entitiesSkinPacksConfig = entitiesSkinPacksConfig;
            _skinsChanger = skinsChanger;
        }
        
        public void Initialize(SkinsPreviewModel model)
        {
            _model = model;
            _model.OnEntitiesSkinPackPreviewChanged += UpdateActiveRow;
            
            InitializeRows();
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

            SetRowsData();
            UpdateActiveRow(_model.EntitiesSkinPackPreview);
        }

        private void UpdateActiveRow(EntitiesSkinPackType skinPackType)
        {
            if (_activeRow != null) 
                _activeRow.ToggleActivity(false);

            _activeRow = _rows.Find(r => r.EntitiesSkinPackType == skinPackType);
            if (_activeRow != null) 
                _activeRow.ToggleActivity(true);
        }
        
        private void ChangeSkinPackPreview(EntitiesSkinPackType newEntitiesSkinPack) 
            => _model.ChangeEntityPreview(newEntitiesSkinPack);

        private void SetRowsData()
        {
            var skinsPacks = _entitiesSkinPacksConfig.SkinsPacks.Keys.ToList();
            for (int i = 0; i < _entitiesSkinPacksConfig.SkinsPacks.Count && i < _rows.Count; i++)
            {
                var key = skinsPacks[i];
                _rows[i].SetData(_skinsChanger, key, _entitiesSkinPacksConfig.SkinsPacks[key]);
            }
        }

        private void OnDestroy()
        {
            _model.OnEntitiesSkinPackPreviewChanged -= UpdateActiveRow;
        }
    }
}