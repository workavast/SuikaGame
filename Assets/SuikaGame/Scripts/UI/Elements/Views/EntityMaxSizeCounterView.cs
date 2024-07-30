using SuikaGame.Scripts.Entities.MaxSizeCounting;
using TMPro;
using UnityEngine;
using Zenject;

namespace SuikaGame.Scripts.UI.Elements.Views
{
    public class EntityMaxSizeCounterView : MonoBehaviour
    {
        [SerializeField] private TMP_Text tmpText;

        private IEntityMaxSizeCounter _entityMaxSizeCounter;
        
        [Inject]
        public void Construct(IEntityMaxSizeCounter entityMaxSizeCounter)
        {
            _entityMaxSizeCounter = entityMaxSizeCounter;
            _entityMaxSizeCounter.OnCurrentMaxSizeChanged += UpdateCounter;
        }

        private void Awake() 
            => UpdateCounter(_entityMaxSizeCounter.CurrentMaxSize);

        private void UpdateCounter(int currentValue) 
            => tmpText.text = $"{currentValue}";
    }
}