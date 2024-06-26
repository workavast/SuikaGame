using SuikaGame.Scripts.Score;
using TMPro;
using UnityEngine;
using Zenject;

namespace SuikaGame.Scripts.UI
{
    [DisallowMultipleComponent]
    [RequireComponent(typeof(TMP_Text))]
    public class RecordCounterView : MonoBehaviour
    {
        private TMP_Text _tmpText;
        private IScoreCounter _scoreCounter;
        
        [Inject]
        public void Construct(IScoreCounter scoreCounter)
        {
            _scoreCounter = scoreCounter;
            _scoreCounter.OnRecordChanged += UpdateRecord;
        }

        private void Awake()
        {
            _tmpText = GetComponent<TMP_Text>();
            UpdateRecord(_scoreCounter.Score);
        }

        private void UpdateRecord(int currentRecord) 
            => _tmpText.text = $"{currentRecord}";
    }
}