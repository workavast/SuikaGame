using UnityEngine;
using UnityEngine.UI;

namespace SuikaGame.Scripts.UI.Windows.Skins.Preview
{
    [RequireComponent(typeof(Image))]
    public class EntitySkinPreview : MonoBehaviour
    {
        private Image _skinPreview;

        private void Awake() 
            => _skinPreview = GetComponent<Image>();

        public void SetNewSkin(Sprite newSkin) 
            => _skinPreview.sprite = newSkin;
    }
}