using SuikaGame.Scripts.Entities.Factory;
using SuikaGame.Scripts.GameplayControlling;
using SuikaGame.Scripts.GameplayField.Model;
using SuikaGame.Scripts.Leaderboard;
using SuikaGame.Scripts.ScenesLoading;
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
        private ILeaderBoardPositionLoader _leaderBoardPositionLoader;
        
        [Inject]
        public void Construct(ISceneLoader sceneLoader, IEntityFactory entityFactory, 
            IGameplayFieldReadModel gameplayFieldReadModel, IGameplayController gameplayController, 
            ILeaderBoardPositionLoader leaderBoardPositionLoader)
        {
            _sceneLoader = sceneLoader;
            _entityFactory = entityFactory;
            _gameplayFieldReadModel = gameplayFieldReadModel;
            _gameplayController = gameplayController;
            _leaderBoardPositionLoader = leaderBoardPositionLoader;
        }

        private void Start()
        {
            Debug.Log("-||- GameplaySceneBootstrap");
            
            _leaderBoardPositionLoader.LoadLeaderboardPosition();
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