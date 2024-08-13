using UnityEngine;
using Zenject;

namespace SuikaGame.Scripts.Leaderboard
{
    public class LeaderboardInstaller : MonoInstaller
    {
        [SerializeField] private LeaderboardConfig leaderboardConfig;
        
        public override void InstallBindings()
        {
            BindConfig();
            BindPositionLoader();
        }

        private void BindConfig()
        {
            Container.BindInstance(leaderboardConfig).AsSingle().NonLazy();
        }
        
        private void BindPositionLoader()
        {
            Container.BindInterfacesTo<LeaderBoardPositionLoader>().AsSingle().NonLazy();
        }
    }
}