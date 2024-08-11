using SuikaGame.Scripts.Ad.RewardedAd;
using SuikaGame.Scripts.Coins;
using SuikaGame.Scripts.UI.Elements.Buttons;
using TMPro;
using UnityEngine;
using Zenject;

namespace SuikaGame.Scripts.UI.Elements.Views
{
    public class GetedCoinsView : MonoBehaviour
    {
        [SerializeField] private TMP_Text tmpText;
        [SerializeField] private SomeButton doubleCoins;
        
        private int _getedCoinsValue;
        private IRewardedAd _rewardedAd;
        private ICoinsModel _coinsModel;

        [Inject]
        public void Construct(ICoinsModel coinsModel, IRewardedAd rewardedAd)
        {
            _rewardedAd = rewardedAd;
            _coinsModel = coinsModel;
            
            _rewardedAd.OnAdReward += OnRewarded;
        }

        private void Awake()
        {
            doubleCoins.OnClick += DoubleCoinsAd;
        }

        public void SetValue(int score)
        {
            _getedCoinsValue = _coinsModel.CoinsByScore(score);
            tmpText.text = _getedCoinsValue.ToString();
            doubleCoins.SetText($"+{_getedCoinsValue.ToString()}");
            doubleCoins.ToggleActivity(true);
        }

        private void DoubleCoinsAd() 
            => _rewardedAd.Show(RewardIds.DoubleCoins);
        
        private void OnRewarded(string rewardId)
        {
            if (rewardId == RewardIds.DoubleCoins)
                DoubleCoins();
        }
        
        private void DoubleCoins()
        {
            doubleCoins.ToggleActivity(false);
            _coinsModel.ChangeCoinsValue(_getedCoinsValue);
            _getedCoinsValue *= 2;

            tmpText.text = _getedCoinsValue.ToString();
        }

        private void OnDestroy()
        {
            doubleCoins.OnClick -= DoubleCoinsAd;
        }
    }
}