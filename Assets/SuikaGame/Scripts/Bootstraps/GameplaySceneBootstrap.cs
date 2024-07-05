using SuikaGame.Scripts.Loading;
using UnityEngine;
using Zenject;

namespace SuikaGame.Scripts.Bootstraps
{
    public class GameplaySceneBootstrap : MonoBehaviour
    {
        private ISceneLoader _sceneLoader;
        
        [Inject]
        public void Construct(ISceneLoader sceneLoader)
        {
            _sceneLoader = sceneLoader;
        }

        private void Start()
        {
            Debug.Log("-||- GameplaySceneBootstrap");
            _sceneLoader.Initialize(false);
        }
    }
}