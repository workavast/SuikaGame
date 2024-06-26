using SuikaGame.Scripts.Score;
using TMPro;
using UnityEngine;
using Zenject;

namespace SuikaGame.Scripts.UI
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
            _scoreCounter.OnScoreChanged += UpdateScore;
        }

        private void Awake()
        {
            _tmpText = GetComponent<TMP_Text>();
            UpdateScore(_scoreCounter.Score);
        }

        private void UpdateScore(int currentScore) 
            => _tmpText.text = $"{currentScore}";
    }
}