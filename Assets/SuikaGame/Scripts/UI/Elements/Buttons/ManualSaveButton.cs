using SuikaGame.Scripts.GameplayField.Savers.ManualSaver;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace SuikaGame.Scripts.UI.Elements.Buttons
{
    [RequireComponent(typeof(Button))]
    public class ManualSaveButton : MonoBehaviour
    {
        [SerializeField] private GameObject unAllowSave;
        
        private IManualGameplaySaver _manualGameplaySaver;
        
        [Inject]
        public void Construct(IManualGameplaySaver manualGameplaySaver)
        {
            _manualGameplaySaver = manualGameplaySaver;

            _manualGameplaySaver.ReadOnlyTimer.OnTimerEnd += UnBlockButton;
        }
        
        private void Awake()
        {
            GetComponent<Button>().onClick.AddListener(() =>
            {
                _manualGameplaySaver.Save();
                BlockButton();
            });

            if (_manualGameplaySaver.SaveAllowed)
                UnBlockButton();
            else
                BlockButton();
        }

        private void UnBlockButton() 
            => unAllowSave.SetActive(false);

        private void BlockButton()
            => unAllowSave.SetActive(true);
    }
}