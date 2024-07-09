using GamePush;
using UnityEngine;

namespace SuikaGame.Scripts.Leaderboard
{
    [CreateAssetMenu(fileName = nameof(LeaderboardConfig), menuName = "SuikaGame/Configs/" + nameof(LeaderboardConfig))]
    public class LeaderboardConfig : ScriptableObject
    {
        [field: SerializeField, Min(0)] public int Limit { get; private set; } = 10;
        [field: SerializeField, Min(0)] public int ShowNearest { get; private set; } = 5;
        [field: SerializeField] public WithMe WithMe { get; private set; } = WithMe.none;
        [field: SerializeField] public Order Order { get; private set; } = Order.DESC;
    }
}