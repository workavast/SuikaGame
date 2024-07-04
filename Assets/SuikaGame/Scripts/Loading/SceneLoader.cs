using System;
using UnityEngine.SceneManagement;
using Zenject;

namespace SuikaGame.Scripts.Loading
{
    public class SceneLoader : ISceneLoader
    {
        public int LoadingSceneIndex => LOADING_SCENE_INDEX;
        public int BootstrapSceneIndex => BOOTSTRAP_SCENE_INDEX;
        
        private const int LOADING_SCENE_INDEX = 1;
        private const int BOOTSTRAP_SCENE_INDEX = 0;
        private int _targetSceneIndex = -1;
        private readonly ILoadingScreen _loadingScreen;
        
        public event Action LoadingStarted;

        [Inject]
        public SceneLoader(ILoadingScreen loadingScreen)
        {
            _loadingScreen = loadingScreen;
        }
        
        public void Initialize(bool endInstantly)
        {
            var activeSceneIndex = SceneManager.GetActiveScene().buildIndex;
            
            if(activeSceneIndex == BootstrapSceneIndex)
                return;
                
            if (_targetSceneIndex <= -1 || activeSceneIndex == _targetSceneIndex)
                EndLoading(endInstantly);

            if (activeSceneIndex == LoadingSceneIndex)
                StartLoadTargetScene();
        }
        
        public void LoadScene(int index)
        {
            _targetSceneIndex = index;
            
            LoadingStarted?.Invoke();
            
            _loadingScreen.StartLoading();
            SceneManager.LoadSceneAsync(LoadingSceneIndex);
        }

        private void EndLoading(bool endInstantly)
        {
            if(endInstantly)
                _loadingScreen.EndLoadingInstantly();
            else
                _loadingScreen.EndLoading();
        }
        
        private void StartLoadTargetScene() 
            => SceneManager.LoadSceneAsync(_targetSceneIndex);
    }
}