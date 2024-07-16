using SuikaGame.Scripts.Coins;
using SuikaGame.Scripts.Score;
using UnityEngine;
using Zenject;

namespace SuikaGame.Scripts.UI
{
    public class NewRecordTitle : MonoBehaviour
    {
        private IScoreCounter _scoreCounter;
        private ICoinsController _coinsController;
        
        [Inject]
        public void Construct(IScoreCounter scoreCounter, ICoinsController coinsController)
        {
            _scoreCounter = scoreCounter;
            _coinsController = coinsController;
        }

        public void TryShow()
        {
            _coinsController.AddCoinsByScore(_scoreCounter.Score);
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