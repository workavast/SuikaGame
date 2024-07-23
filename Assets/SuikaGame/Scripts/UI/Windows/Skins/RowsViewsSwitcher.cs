using System;
using SuikaGame.Scripts.UI.Windows.Skins.Backgrounds;
using SuikaGame.Scripts.UI.Windows.Skins.Entities;
using UnityEngine;

namespace SuikaGame.Scripts.UI.Windows.Skins
{
    public class RowsViewsSwitcher : MonoBehaviour
    {
        [SerializeField] private EntitiesSkinPacksRowsView entitiesSkinPacksRowsView;
        [SerializeField] private BackgroundsSkinsRowsView backgroundsSkinsRowsView;
        [SerializeField] private GameObject entityGlow;
        [SerializeField] private GameObject backgroundGlow;
        
        public event Action OnEntitiesScrollOpen;
        public event Action OnBackgroundScrollOpen;
        
        public void Initialize() 
            => _OpenEntitiesSkins();

        public void _OpenEntitiesSkins()
        {
            entitiesSkinPacksRowsView.gameObject.SetActive(true);
            backgroundsSkinsRowsView.gameObject.SetActive(false);
            entityGlow.SetActive(true);
            backgroundGlow.SetActive(false);
            OnEntitiesScrollOpen?.Invoke();
        }
        
        public void _OpenBackgroundsSkins()
        {
            entitiesSkinPacksRowsView.gameObject.SetActive(false);
            backgroundsSkinsRowsView.gameObject.SetActive(true);
            entityGlow.SetActive(false);
            backgroundGlow.SetActive(true);
            OnBackgroundScrollOpen?.Invoke();
        }
    }
}