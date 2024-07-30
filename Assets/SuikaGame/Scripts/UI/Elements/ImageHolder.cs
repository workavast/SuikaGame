using UnityEngine;
using UnityEngine.UI;

namespace SuikaGame.Scripts.UI.Elements
{
    [RequireComponent(typeof(Image))]
    public class ImageHolder : MonoBehaviour
    {
        public Image Image { get; private set; }

        public Sprite sprite
        {
            get => Image.sprite;
            set => Image.sprite = value;
        }

        private void Awake() 
            => Image = GetComponent<Image>();
    }
}