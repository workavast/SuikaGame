using System;
using SuikaGame.Scripts.Saves.Coins;
using TMPro;
using UnityEngine;
using Zenject;

namespace SuikaGame.Scripts.Coins
{
    [RequireComponent(typeof(TMP_Text))]
    public class CoinsView : MonoBehaviour
    {
        private TMP_Text _tmpText;
        private ICoinsReadModel _coinsSettings;
        
        [Inject]
        private void Construct(ICoinsReadModel coinsModel)
        {
            _coinsSettings = coinsModel;
            
            _coinsSettings.OnChange += UpdateView;
        }
        
        private void Awake()
        {
            _tmpText = GetComponent<TMP_Text>();
            UpdateView();
        }

        private void UpdateView() 
            => _tmpText.text = _coinsSettings.Coins.ToString();

        private void OnDestroy() 
            => _coinsSettings.OnChange -= UpdateView;
    }
}