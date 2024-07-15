using System.Collections.Generic;
using SuikaGame.Scripts.Skins.SkinsLoading;
using UnityEngine;
using Zenject;

namespace SuikaGame.Scripts.MergeProgress
{
    public class MergeProgressView : MonoBehaviour
    {
        private ISkinsLoader _skinsLoader;
        private readonly List<MergeProgressCell> _cells = new(10);
        
        [Inject]
        public void Construct(ISkinsLoader skinsLoader)
        {
            _skinsLoader = skinsLoader;
            
            _skinsLoader.OnEntitiesSkinsLoaded += UpdateSkins;
        }

        private void Awake()
        {
            _cells.AddRange(GetComponentsInChildren<MergeProgressCell>(true));
        }

        private void Start()
        {
            UpdateSkins();
        }

        private void UpdateSkins()
        {
            if (_skinsLoader.EntitiesSkinPackConfig == null)
                return;
            
            for (int i = 0; i < _skinsLoader.EntitiesSkinPackConfig.Skins.Count && i < _cells.Count; i++)
                _cells[i].SetSkin(_skinsLoader.EntitiesSkinPackConfig.Skins[i]);
        }
    }
}