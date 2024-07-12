using System.Collections.Generic;
using System.Linq;
using SuikaGame.Scripts.Skins.Backgrounds;
using UnityEngine;
using Zenject;

namespace SuikaGame.Scripts.UI.Windows.Skins.Backgrounds
{
    public class BackgroundsSkinsRowsView : MonoBehaviour
    {
        [SerializeField] private Transform skinPacksParent;
        [SerializeField] private BackgroundSkinPreviewRow backgroundSkinPreviewRowPrefab;

        private readonly List<BackgroundSkinPreviewRow> _rows = new(4);
        private BackgroundsSkinsConfig _backgroundsSkinsConfig;
        private SkinsPreviewModel _model;

        [Inject]
        public void Construct(BackgroundsSkinsConfig backgroundsSkinsConfig)
        {
            _backgroundsSkinsConfig = backgroundsSkinsConfig;
        }

        public void Initialize(SkinsPreviewModel model)
        {
            _model = model;
            InitializeRows();
            SetRowsData();
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

        private void ChangeSkinPreview(BackgroundSkinType newSkinPack) 
            => _model.ChangeBackgroundPreview(newSkinPack);

        private void SetRowsData()
        {
            if(_backgroundsSkinsConfig.BackgroundsSkins.Count != _rows.Count)
                InitializeRows();

            var backgroundSkinTypes = _backgroundsSkinsConfig.BackgroundsSkins.Keys.ToList();
            for (int i = 0; i < _backgroundsSkinsConfig.BackgroundsSkins.Count && i < _rows.Count; i++)
            {
                var key = backgroundSkinTypes[i];
                _rows[i].SetData(key, _backgroundsSkinsConfig.BackgroundsSkins[key]);
            }
        }
    }
}