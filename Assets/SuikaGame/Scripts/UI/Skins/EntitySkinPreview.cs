using UnityEngine;
using UnityEngine.UI;

namespace SuikaGame.Scripts.UI.Skins
{
    [RequireComponent(typeof(Image))]
    public class EntitySkinPreview : MonoBehaviour
    {
        private Image _image;

        private void Awake() 
            => _image = GetComponent<Image>();

        public void SetNewSkin(Sprite newSkin) 
            => _image.sprite = newSkin;
    }
}