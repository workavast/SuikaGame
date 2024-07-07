using GamePush;
using SuikaGame.Scripts.Game;
using UnityEngine;

namespace SuikaGame.Scripts.UI
{
    public class SettingsWindow : MonoBehaviour
    {
        [SerializeField] private int limit = 10;
        [SerializeField] private int showNearest = 5;
        [SerializeField] private WithMe withMe = WithMe.none;
        [SerializeField] private Order order = Order.DESC;
        
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

        public void OpenLeaderboard()
        {
            GP_Leaderboard.Open(
                order: order,
                limit: limit,
                showNearest: showNearest,
                withMe: withMe
                );
        }
    }
}