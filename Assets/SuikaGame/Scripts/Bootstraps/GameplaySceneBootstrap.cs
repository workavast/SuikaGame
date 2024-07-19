using SuikaGame.Scripts.Entities.Factory;
using SuikaGame.Scripts.GameplayControlling;
using SuikaGame.Scripts.GameplayField;
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
        private IGameplayFieldReadModel _gameplayFieldReadModel;
        private IGameplayController _gameplayController;
        
        [Inject]
        public void Construct(ISceneLoader sceneLoader, IEntityFactory entityFactory, 
            IGameplayFieldReadModel gameplayFieldReadModel, IGameplayController gameplayController)
        {
            _sceneLoader = sceneLoader;
            _entityFactory = entityFactory;
            _gameplayFieldReadModel = gameplayFieldReadModel;
            _gameplayController = gameplayController;
        }

        private void Start()
        {
            Debug.Log("-||- GameplaySceneBootstrap");
            
            LoadPrevGameplaySession();
            _gameplayController.Initialize();
            _sceneLoader.Initialize(false);
        }
        
        private void LoadPrevGameplaySession()
        {
            var entityModels = _gameplayFieldReadModel.EntityModels;

            foreach (var entityModel in entityModels)
                _entityFactory.Create(entityModel.sizeIndex, entityModel.Vector2Position, entityModel.rotation);
        }
    }
}