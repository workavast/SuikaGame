using System.Collections.Generic;
using System.Linq;
using SuikaGame.Scripts.Skins.Entities;
using UnityEngine;
using Zenject;

namespace SuikaGame.Scripts.UI.Windows.Skins.Entities
{
    public class EntitiesSkinPacksRowsView : MonoBehaviour
    {
        [SerializeField] private Transform skinsPacksParent;
        [SerializeField] private EntitiesSkinPackPreviewRow entitiesSkinPackPreviewRowPrefab;

        private EntitiesSkinPacksConfig _entitiesSkinPacksConfig;
        private readonly List<EntitiesSkinPackPreviewRow> _rows = new(8);
        private SkinsPreviewModel _model;

        [Inject]
        public void Construct(EntitiesSkinPacksConfig entitiesSkinPacksConfig)
        {
            _entitiesSkinPacksConfig = entitiesSkinPacksConfig;
        }
        
        public void Initialize(SkinsPreviewModel model)
        {
            _model = model;
            InitializeRows();
            LoadSkinsPacksRows();
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
            {
                existRows.RemoveAt(i);
            }            
            
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
            => _model.ChangeEntityPreview(newEntitiesSkinPack);

        private void LoadSkinsPacksRows()
        {
            if(_entitiesSkinPacksConfig.SkinsPacks.Count != _rows.Count)
                InitializeRows();

            var skinsPacks = _entitiesSkinPacksConfig.SkinsPacks.Keys.ToList();
            for (int i = 0; i < _entitiesSkinPacksConfig.SkinsPacks.Count && i < _rows.Count; i++)
            {
                var key = skinsPacks[i];
                _rows[i].SetData(key, _entitiesSkinPacksConfig.SkinsPacks[key]);
            }
        }
    }
}