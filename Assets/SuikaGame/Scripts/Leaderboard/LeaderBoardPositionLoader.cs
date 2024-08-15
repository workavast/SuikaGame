using System;
using Avastrad.CustomTimer;
using GamePush;
using SuikaGame.Scripts.Score;
using UnityEngine;
using Zenject;

namespace SuikaGame.Scripts.Leaderboard
{
    public class LeaderBoardPositionLoader : ILeaderBoardPositionLoader, ITickable, IDisposable
    {
        private readonly LeaderboardConfig _leaderboardConfig;
        private readonly IScoreCounter _scoreCounter;
        public int Place { get; private set; }
        public bool IsLoading { get; private set; }
        public bool IsLoadSuccess { get; private set; }

        public event Action OnStartLoad;
        public event Action<bool, int> OnPlaceLoaded;

        private readonly Timer _waitTimer;
        
        public LeaderBoardPositionLoader(LeaderboardConfig leaderboardConfig, IScoreCounter scoreCounter)
        {
            _leaderboardConfig = leaderboardConfig;
            _scoreCounter = scoreCounter;

            _waitTimer = new Timer(1, 0, true);
            
            GP_Leaderboard.OnFetchPlayerRatingSuccess += OnLoadSuccess;
            GP_Leaderboard.OnFetchPlayerRatingError += OnLoadError;
            _waitTimer.OnTimerEnd += InvokeRequest;

            _scoreCounter.OnRecordChanged += (_) => LoadLeaderboardPosition();
        }
        
        public void LoadLeaderboardPosition()
        {
            IsLoading = true;
            OnStartLoad?.Invoke();
            _waitTimer.Reset();
        }

        public void Tick()
        {
            _waitTimer.Tick(Time.unscaledDeltaTime);
        }

        private void InvokeRequest()
        {
            GP_Leaderboard.FetchPlayerRating("", "score", _leaderboardConfig.Order);
        }
        
        private void OnLoadSuccess(string str, int  data)
        {
            _waitTimer.SetPause();
            IsLoading = false;
            IsLoadSuccess = true;
            Place = data;
            OnPlaceLoaded?.Invoke(true, Place);
        }
        
        private void OnLoadError()
        {
            _waitTimer.SetPause();
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