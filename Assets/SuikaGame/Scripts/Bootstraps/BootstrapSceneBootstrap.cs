using System.Collections;
using System.Threading.Tasks;
using GamePush;
using SuikaGame.Scripts.Loading;
using UnityEngine;
using Zenject;

namespace SuikaGame.Scripts.Bootstraps
{
    public class BootstrapSceneBootstrap : MonoBehaviour
    {
        [SerializeField] private int sceneIndexForLoadingAfterInitializations;
        
        private ISceneLoader _sceneLoader;
        
        [Inject]
        public void Construct(ISceneLoader sceneLoader)
        {
            _sceneLoader = sceneLoader;
            Debug.Log("SAD");
        }
        
        private async void Start()
        {
            Debug.Log("-||- BootstrapSceneBootstrap start");

            if (!GP_Init.isReady)
                StartCoroutine(Init());
            else
            {
                _sceneLoader.Initialize(false);
                Debug.Log("-||- BootstrapSceneBootstrap loadScene in start");
                _sceneLoader.LoadScene(sceneIndexForLoadingAfterInitializations);
            }
            
            // if (!GP_Init.isReady)
            //     await InitGamePush();
            //
            // _sceneLoader.Initialize(false);
            // Debug.Log("-||- BootstrapSceneBootstrap loadScene");
            // _sceneLoader.LoadScene(sceneIndexForLoadingAfterInitializations);
        }

        private static async Task InitGamePush()
        {
            Debug.Log("-||- BootstrapSceneBootstrap Try INIT");
            await GP_Init.Ready;

            if (!GP_Init.isReady)
                await InitGamePush();
        }

        private IEnumerator Init()
        {
            Debug.Log("-||- BootstrapSceneBootstrap Try INIT");
            yield return GP_Init.Ready;

            if (!GP_Init.isReady)
            {
                Debug.Log("-||- BootstrapSceneBootstrap dontInited INIT");
                yield return Init();
            }
            else
            {
                _sceneLoader.Initialize(false);
                Debug.Log("-||- BootstrapSceneBootstrap loadScene in init");
                _sceneLoader.LoadScene(sceneIndexForLoadingAfterInitializations);
            }
        }
    }
}