using SuikaGame.Scripts.Score;
using TMPro;
using UnityEngine;
using Zenject;

namespace SuikaGame.Scripts.UI.Elements.Views
{
    [DisallowMultipleComponent]
    [RequireComponent(typeof(TMP_Text))]
    public class ScoreCounterView : MonoBehaviour
    {
        private TMP_Text _tmpText;
        private IScoreCounter _scoreCounter;
        
        [Inject]
        public void Construct(IScoreCounter scoreCounter)
        {
            _scoreCounter = scoreCounter;
        }

        private void Awake()
        {
            _tmpText = GetComponent<TMP_Text>();
        }

        private void UpdateScore(int currentScore) 
            => _tmpText.text = currentScore.ToString();
        
        private void OnEnable()
        {
            _scoreCounter.OnScoreChanged += UpdateScore;
            UpdateScore(_scoreCounter.Score);
        }

        private void OnDisable()
        {
            if (_scoreCounter != null)
                _scoreCounter.OnScoreChanged += UpdateScore;
        }
    }
}