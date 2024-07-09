using SuikaGame.Scripts.Game;
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
            GamePauser.Pause();
            gameObject.SetActive(true);
        }

        public void Hide()
        {
            GamePauser.Continue();
            gameObject.SetActive(false);
        }
    }
}