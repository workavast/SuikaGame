using UnityEngine;

namespace SuikaGame.Scripts.UI
{
    public class SettingsWindow : MonoBehaviour
    {
        private void Awake()
        {
            Hide();
        }

        public void Show()
        {
            gameObject.SetActive(true);
        }

        public void Hide()
        {
            gameObject.SetActive(false);
        }
    }
}