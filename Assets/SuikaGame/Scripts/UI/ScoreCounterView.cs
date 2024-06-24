using SuikaGame.Scripts.Score;
using TMPro;
using UnityEngine;
using Zenject;

namespace SuikaGame.UI
{
    public class ScoreCounterView : MonoBehaviour
    {
        [SerializeField] private TMP_Text tmpText;

        private IScoreCounter _scoreCounter;
        
        [Inject]
        public void Construct(IScoreCounter scoreCounter)
        {
            _scoreCounter = scoreCounter;
            _scoreCounter.OnScoreChanged += UpdateScore;
        }

        private void Awake() 
            => UpdateScore(_scoreCounter.Score);

        private void UpdateScore(int currentScore) 
            => tmpText.text = $"{currentScore}";
    }
}