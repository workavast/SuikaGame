using UnityEngine;

namespace SuikaGame.Scripts
{
    public class BackgroundHolder : MonoBehaviour
    {
        [SerializeField] private SpriteRenderer spriteRenderer;
        
        public void ChangeSkin(Sprite newSkin) 
            => spriteRenderer.sprite = newSkin;
    }
}