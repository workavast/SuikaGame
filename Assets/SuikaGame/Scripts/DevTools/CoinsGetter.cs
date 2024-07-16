using SuikaGame.Scripts.Coins;
using UnityEngine;
using Zenject;

namespace SuikaGame.Scripts.DevTools
{
    public class CoinsGetter : MonoBehaviour
    {
        [Inject] private ICoinsChangeModel _coinsChangeModel;
        
        [ContextMenu("Get 500 Coins")]
        private void GetCoins() 
            => _coinsChangeModel.ChangeCoinsValue(500);
    }
}