using SuikaGame.Scripts.UI.Windows.Skins.Backgrounds;
using SuikaGame.Scripts.UI.Windows.Skins.Entities;
using UnityEngine;

namespace SuikaGame.Scripts.UI.Windows.Skins
{
    public class RowsViewsSwitcher : MonoBehaviour
    {
        [SerializeField] private EntitiesSkinPacksRowsView entitiesSkinPacksRowsView;
        [SerializeField] private BackgroundsSkinsRowsView backgroundsSkinsRowsView;
        
        public void _OpenEntitiesSkins()
        {
            entitiesSkinPacksRowsView.gameObject.SetActive(true);
            backgroundsSkinsRowsView.gameObject.SetActive(false);
        }
        
        public void _OpenBackgroundsSkins()
        {
            entitiesSkinPacksRowsView.gameObject.SetActive(false);
            backgroundsSkinsRowsView.gameObject.SetActive(true);
        }
    }
}