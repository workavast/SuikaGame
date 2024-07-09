using UnityEngine;

namespace SuikaGame.Scripts.UI.Windows
{
    public class SkinsWindow : MonoBehaviour
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