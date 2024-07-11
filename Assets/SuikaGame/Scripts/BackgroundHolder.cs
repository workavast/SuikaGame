using UnityEngine;

namespace SuikaGame.Scripts
{
    [RequireComponent(typeof(SpriteRenderer))]
    public class BackgroundHolder : MonoBehaviour
    {
        private SpriteRenderer _spriteRenderer;
        
        private void Awake() 
            => _spriteRenderer = GetComponent<SpriteRenderer>();

        public void ChangeSkin(Sprite newSkin) 
            => _spriteRenderer.sprite = newSkin;
    }
}