using SuikaGame.Scripts.Score;
using UnityEngine;
using Zenject;

namespace SuikaGame.Scripts.UI
{
    public class NewRecordTitle : MonoBehaviour
    {
        private IScoreCounter _scoreCounter;
        
        [Inject]
        public void Construct(IScoreCounter scoreCounter)
        {
            _scoreCounter = scoreCounter;
        }

        public void TryShow()
        {
            if (_scoreCounter.Record <= _scoreCounter.Score)
            {
                _scoreCounter.ApplyRecord();
                gameObject.SetActive(true);
            }
            else
                Hide();
        }

        public void Hide() 
            => gameObject.SetActive(false);
    }
}