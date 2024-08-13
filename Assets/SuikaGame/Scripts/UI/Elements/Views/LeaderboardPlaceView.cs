using SuikaGame.Scripts.Leaderboard;
using TMPro;
using UnityEngine;
using Zenject;

namespace SuikaGame.Scripts.UI.Elements.Views
{
    public class LeaderboardPlaceView : MonoBehaviour
    {
        [SerializeField] private TMP_Text view;
        [SerializeField] private GameObject loadTitle;
        [SerializeField] private GameObject loadErrorTitle;
        
        private ILeaderBoardPositionLoader _leaderBoardPositionLoader;
        
        [Inject]
        public void Construct(ILeaderBoardPositionLoader leaderBoardPositionLoader)
        {
            _leaderBoardPositionLoader = leaderBoardPositionLoader;
            _leaderBoardPositionLoader.OnStartLoad += OnStartLoad;
            _leaderBoardPositionLoader.OnPlaceLoaded += UpdateView;
        }

        private void Awake()
        {
            OnStartLoad();
            if (!_leaderBoardPositionLoader.IsLoading) 
                UpdateView(_leaderBoardPositionLoader.IsLoadSuccess, _leaderBoardPositionLoader.Place);
        }

        private void OnStartLoad()
        {
            view.gameObject.SetActive(false);
            loadTitle.SetActive(true);
            loadErrorTitle.SetActive(false);
        }
        
        private void UpdateView(bool placeLoadSuccess, int place)
        {
            loadTitle.SetActive(false);
            if (placeLoadSuccess)
            {
                loadErrorTitle.SetActive(true);
            }
            else
            {
                view.gameObject.SetActive(true);
                view.text = place.ToString();
            }
        }

        private void OnDestroy()
        {
            _leaderBoardPositionLoader.OnStartLoad -= OnStartLoad;
            _leaderBoardPositionLoader.OnPlaceLoaded -= UpdateView;
        }
    }
}