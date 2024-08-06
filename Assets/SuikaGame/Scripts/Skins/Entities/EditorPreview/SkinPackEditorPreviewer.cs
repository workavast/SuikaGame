using UnityEngine;

namespace SuikaGame.Scripts.Skins.Entities.EditorPreview
{
    public class SkinPackEditorPreviewer : MonoBehaviour
    {
        [SerializeField] private EntitiesSkinPackConfig config;
        
        [ContextMenu("Load Skin Preview")]
        public void LoadSkinPreview()
        {
            var spriteRenderers = GetComponentsInChildren<SpriteRenderer>();
            for (int i = 0; i < config.Skins.Count && i < spriteRenderers.Length; i++)
                spriteRenderers[i].sprite = config.Skins[i];
        }
    }
}