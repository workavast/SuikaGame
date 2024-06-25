using SuikaGame.Scripts;
using SuikaGame.Scripts.GameControlling;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace SuikaGame.UI
{
    [DisallowMultipleComponent]
    [RequireComponent(typeof(Button))]
    public class ResetButton : MonoBehaviour
    {
        private Button _resetButton;
        private IGameReseter _gameReseter;
        
        [Inject]
        public void Construct(IGameReseter gameReseter)
        {
            _gameReseter = gameReseter;
        }
        
        private void Awake()
        {
            _resetButton = GetComponent<Button>();
            _resetButton.onClick.AddListener(() => { _gameReseter.ResetGame(); });
        }
    }
}