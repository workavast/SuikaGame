using System;
using System.Collections;
using GamePush;
using SuikaGame.Scripts.Audio;
using SuikaGame.Scripts.Localization;
using SuikaGame.Scripts.Saves;
using SuikaGame.Scripts.ScenesLoading;
using UnityEngine;
using Zenject;

namespace SuikaGame.Scripts.Bootstraps
{
    public class BootstrapSceneBootstrap : MonoBehaviour
    {
        [SerializeField] private int sceneIndexForLoadingAfterInitializations;
        
        private ISceneLoader _sceneLoader;
        private AudioVolumeChanger _audioVolumeChanger;
        
        [Inject]
        public void Construct(ISceneLoader sceneLoader, AudioVolumeChanger audioVolumeChanger)
        {
            _sceneLoader = sceneLoader;
            _audioVolumeChanger = audioVolumeChanger;
        }
        
        private void Start()
        {
            Debug.Log("-||- BootstrapSceneBootstrap start");
            InitializeGamePush(() => 
                InitializeSave(() => 
                    InitializeLanguage( () => 
                        InitializeAudio(
                            LoadNextScene))));
        }

        private void InitializeGamePush(Action onComplete)
        {
            Debug.Log("InitializeGamePush started");
            if (GP_Init.isReady)
                onComplete?.Invoke();
            else
                StartCoroutine(WaitGamePushInitialization(onComplete));
        }
        
        private static void InitializeSave(Action onComplete)
        {
            Debug.Log("InitializeSave started");
            Action onCompleteLambda = null;
            onCompleteLambda = () =>
            {
                PlayerData.Instance.OnInit -= onCompleteLambda;
                onComplete?.Invoke();
            };
            
            PlayerData.Instance.OnInit += onCompleteLambda;
            PlayerData.Instance.InvokeLoad();
        }

        private static void InitializeLanguage(Action onComplete)
        {
            var localizationInitializer = new LocalizationInitializer();
            localizationInitializer.InitLocalizationSettings(PlayerData.Instance.LocalizationSettings, onComplete);
        }

        private void InitializeAudio(Action onComplete)
        {
            _audioVolumeChanger.StartInit();
            onComplete?.Invoke();
        }
        
        private void LoadNextScene()
        {
            Debug.Log("LoadNextScene started");
            _sceneLoader.Initialize(false);
            _sceneLoader.LoadScene(sceneIndexForLoadingAfterInitializations);
        }
        
        private static IEnumerator WaitGamePushInitialization(Action onComplete)
        {
            yield return GP_Init.Ready;

            if (GP_Init.isReady)
                onComplete?.Invoke();
            else
                yield return WaitGamePushInitialization(onComplete);
        }
    }
}



