using SuikaGame.Scripts.GameOver.GameOverControlling;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace SuikaGame.Scripts.UI.Elements.Buttons
{
    [DisallowMultipleComponent]
    [RequireComponent(typeof(Button))]
    public class ManualGameOverButton : MonoBehaviour
    {
        private Button _resetButton;
        private IGameOverInvoker _gameOverInvoker;
        
        [Inject]
        public void Construct(IGameOverInvoker gameReseter)
        {
            _gameOverInvoker = gameReseter;
        }
        
        private void Awake()
        {
            _resetButton = GetComponent<Button>();
            _resetButton.onClick.AddListener(() => { _gameOverInvoker.ManualGameOver(); });
        }
    }
}