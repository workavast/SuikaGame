using System;
using GamePush;
using SuikaGame.Scripts.Leaderboard;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace SuikaGame.Scripts.UI.Elements
{
    [DisallowMultipleComponent]
    [RequireComponent(typeof(Button))]
    public class LeaderboardButton : MonoBehaviour
    {
        private LeaderboardConfig _leaderboardConfig;

        [Inject]
        public void Construct(LeaderboardConfig leaderboardConfig)
        {
            _leaderboardConfig = leaderboardConfig;
        }

        private void Awake()
        {
            var button = GetComponent<Button>();
            button.onClick.AddListener(ShowLeaderboard);
        }

        private void ShowLeaderboard()
        {
            GP_Leaderboard.Open(
                order: _leaderboardConfig.Order,
                limit: _leaderboardConfig.Limit,
                showNearest: _leaderboardConfig.ShowNearest,
                withMe: _leaderboardConfig.WithMe
            );
        }
    }
}