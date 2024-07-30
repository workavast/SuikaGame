using UnityEngine;
using UnityEngine.UI;

namespace SuikaGame.Scripts.UI.Elements.Buttons
{
    [RequireComponent(typeof(Button))]
    public class ActivityToggleButton : MonoBehaviour
    {
        [SerializeField] private bool defaultIsActive;
        [SerializeField] private GameObject toggleElement;
        
        private void Awake()
        {
            GetComponent<Button>().onClick.AddListener(ToggleActivity);
            toggleElement.SetActive(defaultIsActive);
        }

        private void ToggleActivity() 
            => toggleElement.SetActive(!toggleElement.activeSelf);
    }
}