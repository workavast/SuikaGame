using SuikaGame.Scripts.Entities.Factory;
using SuikaGame.Scripts.Loading;
using SuikaGame.Scripts.Saves;
using UnityEngine;
using Zenject;

namespace SuikaGame.Scripts.Bootstraps
{
    public class GameplaySceneBootstrap : MonoBehaviour
    {
        private ISceneLoader _sceneLoader;
        private IEntityFactory _entityFactory;
        
        [Inject]
        public void Construct(ISceneLoader sceneLoader, IEntityFactory entityFactory)
        {
            _sceneLoader = sceneLoader;
            _entityFactory = entityFactory;
        }

        private void Start()
        {
            Debug.Log("-||- GameplaySceneBootstrap");
            
            LoadPrevGameplaySession();
            _sceneLoader.Initialize(false);
        }
        
        private void LoadPrevGameplaySession()
        {
            var entityModels = PlayerData.Instance.GameplaySceneSettings.EntityModels;

            foreach (var entityModel in entityModels)
                _entityFactory.Create(entityModel.sizeIndex, entityModel.Vector2Position, entityModel.rotation);
        }
    }
}