using SuikaGame.Scripts.Score;
using TMPro;
using UnityEngine;
using Zenject;

namespace SuikaGame.Scripts.UI.Elements.Views
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
        }

        private void Awake()
        {
            _tmpText = GetComponent<TMP_Text>();
        }

        private void UpdateRecord(int currentRecord)
            => _tmpText.text = currentRecord.ToString();

        private void OnEnable()
        {
            _scoreCounter.OnRecordChanged += UpdateRecord;
            UpdateRecord(_scoreCounter.Record);
        }

        private void OnDisable()
        {
            if (_scoreCounter != null)
                _scoreCounter.OnRecordChanged += UpdateRecord;
        }
    }
}