using SuikaGame.Scripts.Ad.RewardedAd;
using SuikaGame.Scripts.Coins;
using TMPro;
using UnityEngine;
using Zenject;

namespace SuikaGame.Scripts.UI.Elements
{
    public class GetedCoinsView : MonoBehaviour
    {
        [SerializeField] private TMP_Text tmpText;
        
        private int _getedCoinsValue;
        private IRewardedAd _rewardedAd;
        private ICoinsController _coinsController;

        [Inject]
        public void Construct(ICoinsController coinsChangeModel, IRewardedAd rewardedAd)
        {
            _rewardedAd = rewardedAd;
            _coinsController = coinsChangeModel;
            
            _rewardedAd.OnAdReward += OnRewarded;
        }
        
        public void SetValue(int score)
        {
            _getedCoinsValue = _coinsController.ScoreToCoins(score);
            tmpText.text = _getedCoinsValue.ToString();
        }

        public void _DoubleCoins()
        {
            _rewardedAd.Show(RewardIds.DoubleCoins);
        }

        private void DoubleCoins()
        {
            _coinsController.ChangeCoinsValue(_getedCoinsValue);
            _getedCoinsValue *= 2;

            tmpText.text = _getedCoinsValue.ToString();
        }
        
        private void OnRewarded(string rewardId)
        {
            if (rewardId == RewardIds.DoubleCoins)
                DoubleCoins();
        }
    }
}