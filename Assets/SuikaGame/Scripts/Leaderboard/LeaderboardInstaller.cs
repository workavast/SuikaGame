using UnityEngine;
using Zenject;

namespace SuikaGame.Scripts.Leaderboard
{
    public class LeaderboardInstaller : MonoInstaller
    {
        [SerializeField] private LeaderboardConfig leaderboardConfig;
        
        public override void InstallBindings()
        {
            Container.BindInstance(leaderboardConfig).AsSingle().NonLazy();
        }
    }
}