using System.Collections.Generic;
using System.Linq;
using SuikaGame.Scripts.Skins;
using UnityEngine;
using Zenject;

namespace SuikaGame.Scripts.UI.Skins
{
    public class SkinsPacksRowsView : MonoBehaviour
    {
        [SerializeField] private Transform skinsPacksParent;
        [SerializeField] private SkinPackRow skinPackRowPrefab;

        [Inject] private SkinsPacksConfig _skinsPacksConfig;

        private List<SkinPackRow> _rows;
        private SkinPackPreviewModel _model;
        
        public void Initialize(SkinPackPreviewModel model)
        {
            _model = model;
        }
        
        private void Start()
        {
            InitializeRows();
            LoadSkinsPacksRows();
        }

        private void InitializeRows()
        {
            var existRows = GetComponentsInChildren<SkinPackRow>(true).ToList();
            int existRowsCount = existRows.Count;

            var counter = 0;
            for (int i = 0; i < existRowsCount - _skinsPacksConfig.SkinsPacks.Count; i++)
            {
                counter++;
                Destroy(existRows[i].gameObject);
            }

            for (int i = counter-1; i >= 0; i--)
            {
                existRows.RemoveAt(i);
            }            
            
            _rows = new List<SkinPackRow>(existRows);
            for (int i = existRowsCount; i < _skinsPacksConfig.SkinsPacks.Count; i++)
            {
                var newRow = Instantiate(skinPackRowPrefab, skinsPacksParent); 
                _rows.Add(newRow);
            }

            foreach (var row in _rows)
                row.OnClicked += ChangeActivePreviewSkin;
        }

        private void ChangeActivePreviewSkin(SkinPackType newSkinPack) 
            => _model.ChangeSkinPackPreview(newSkinPack);

        private void LoadSkinsPacksRows()
        {
            if(_skinsPacksConfig.SkinsPacks.Count != _rows.Count)
                InitializeRows();

            var skinsPacks = _skinsPacksConfig.SkinsPacks.Keys.ToList();
            for (int i = 0; i < _skinsPacksConfig.SkinsPacks.Count && i < _rows.Count; i++)
            {
                var key = skinsPacks[i];
                _rows[i].SetData(key, _skinsPacksConfig.SkinsPacks[key]);
            }
        }
    }
}