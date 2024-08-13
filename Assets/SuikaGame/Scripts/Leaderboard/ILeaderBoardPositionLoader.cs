using System;

namespace SuikaGame.Scripts.Leaderboard
{
    public interface ILeaderBoardPositionLoader
    {
        public int Place { get; }
        public bool IsLoading { get; }
        public bool IsLoadSuccess { get; }
        
        public event Action OnStartLoad;
        public event Action<bool, int> OnPlaceLoaded;

        public void LoadLeaderboardPosition();
    }
}