using System;
using GamePush;

namespace SuikaGame.Scripts.Leaderboard
{
    public class LeaderBoardPositionLoader : ILeaderBoardPositionLoader, IDisposable
    {
        public int Place { get; private set; }
        public bool IsLoading { get; private set; }
        public bool IsLoadSuccess { get; private set; }

        public event Action OnStartLoad;
        public event Action<bool, int> OnPlaceLoaded;
        
        public LeaderBoardPositionLoader()
        {
            GP_Leaderboard.OnFetchPlayerRatingSuccess += OnLoadSuccess;
            GP_Leaderboard.OnFetchPlayerRatingError += OnLoadError;
        }
        
        public void LoadLeaderboardPosition()
        {
            IsLoading = true;
            OnStartLoad?.Invoke();
            GP_Leaderboard.FetchPlayerRating();
        }

        private void OnLoadSuccess(string str, int  data)
        {
            IsLoading = false;
            IsLoadSuccess = true;
            Place = data;
            OnPlaceLoaded?.Invoke(true, Place);
        }
        
        private void OnLoadError()
        {
            IsLoading = false;
            IsLoadSuccess = false;
            OnPlaceLoaded?.Invoke(false, Place);
        }

        public void Dispose()
        {
            GP_Leaderboard.OnFetchPlayerRatingSuccess -= OnLoadSuccess;
            GP_Leaderboard.OnFetchPlayerRatingError -= OnLoadError;
        }
    }
}