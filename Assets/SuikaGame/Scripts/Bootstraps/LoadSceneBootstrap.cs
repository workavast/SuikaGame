using SuikaGame.Scripts.Loading;
using UnityEngine;
using Zenject;

namespace SuikaGame.Scripts.Bootstraps
{
    public class LoadSceneBootstrap : MonoBehaviour
    {
        private ISceneLoader _sceneLoader;
        
        [Inject]
        public void Construct(ISceneLoader sceneLoader)
        {
            _sceneLoader = sceneLoader;
        }

        private void Start()
        {
            Debug.Log("-||- LoadSceneBootstrap start");
            _sceneLoader.Initialize(false);
        }
    }
}