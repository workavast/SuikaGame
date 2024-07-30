using SuikaGame.Scripts.Entities.Spawning;
using SuikaGame.Scripts.Skins.SkinsLoading;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace SuikaGame.Scripts.UI.Elements.Views
{
    public class NextEntityView : MonoBehaviour
    {
        [SerializeField] private Image entityPreview;
        
        private INextEntitySpawner _nextEntitySpawner;
        private ISkinsLoader _skinsLoader;
        
        [Inject]
        public void Construct(INextEntitySpawner nextEntitySpawner, ISkinsLoader skinsLoader)
        {
            _nextEntitySpawner = nextEntitySpawner;
            _skinsLoader = skinsLoader;

            _nextEntitySpawner.OnNextEntitySizeIndexChange += UpdateViewByIndex;
            _skinsLoader.OnEntitiesSkinsLoaded += UpdateViewBySkin;
        }

        private void UpdateViewByIndex(int index) 
            => UpdateView(index);

        private void UpdateViewBySkin() 
            => UpdateView(_nextEntitySpawner.NextEntitySizeIndex);

        private void UpdateView(int index)
        {
            if (_skinsLoader.EntitiesSkinPackConfig == null)
                return;

            entityPreview.sprite = _skinsLoader.EntitiesSkinPackConfig.Skins[index];
        }
    }
}