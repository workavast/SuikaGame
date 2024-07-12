using System;
using SuikaGame.Scripts.Skins.Backgrounds;
using SuikaGame.Scripts.Skins.Entities;

namespace SuikaGame.Scripts.UI.Windows.Skins
{
    public class SkinsPreviewModel
    {
        public EntitiesSkinPackType EntitiesSkinPackPreview { get; private set; }

        public BackgroundSkinType BackgroundSkinPreview { get; private set; }

        public event Action OnEntitiesSkinPackPreviewChanged;
        public event Action OnBackgroundPreviewChanged;
        
        public void ChangeEntityPreview(EntitiesSkinPackType newSkin)
        {
            if(EntitiesSkinPackPreview == newSkin)
                return;
            
            EntitiesSkinPackPreview = newSkin;
            OnEntitiesSkinPackPreviewChanged?.Invoke();
        }
        
        public void ChangeBackgroundPreview(BackgroundSkinType newSkin)
        {
            if(BackgroundSkinPreview == newSkin)
                return;
            
            BackgroundSkinPreview = newSkin;
            OnBackgroundPreviewChanged?.Invoke();
        }
    }
}