using SuikaGame.Scripts.Loading;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace SuikaGame.Scripts.UI.Elements
{
    [DisallowMultipleComponent]
    [RequireComponent(typeof(Button))]
    public class LoadSceneButton : MonoBehaviour
    {
        [SerializeField, Min(0)] private int sceneIndex;
        
        private ISceneLoader _sceneLoader;

        [Inject]
        private void Construct(ISceneLoader sceneLoader)
        {
            _sceneLoader = sceneLoader;
        }
        
        private void Awake()
        {
            GetComponent<Button>().onClick.AddListener(() => _sceneLoader.LoadScene(sceneIndex));
        }
    }
}