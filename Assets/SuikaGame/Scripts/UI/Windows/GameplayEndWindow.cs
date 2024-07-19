using Avastrad.UI.UI_System;
using SuikaGame.Scripts.Score;
using SuikaGame.Scripts.UI.Elements;
using UnityEngine;
using Zenject;

namespace SuikaGame.Scripts.UI.Windows
{
    public class GameplayEndWindow : UI_ScreenBase
    {
        [SerializeField] private NewRecordTitle newRecordTitle;
        [SerializeField] private GetedCoinsView gotCoinsView;

        private IScoreCounter _scoreCounter;
        
        [Inject]
        public void Construct(IScoreCounter scoreCounter)
        {
            _scoreCounter = scoreCounter;
        }

        public override void Initialize()
            => Hide();
        
        public override void Show()
        {
            gameObject.SetActive(true);
            TryShow();
        }

        private void TryShow()
        {
            gotCoinsView.SetValue(_scoreCounter.Score);
            if (_scoreCounter.Record <= _scoreCounter.Score)
                newRecordTitle.Show();
            else
                newRecordTitle.Hide();
        }
    }
}