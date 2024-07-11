using System;
using SuikaGame.Scripts.Skins;

namespace SuikaGame.Scripts.UI.Skins
{
    public class SkinPackPreviewModel
    {
        public SkinPackType SkinPackPreview { get; private set; }

        public event Action OnSkinPackPreviewChanged;
        
        public void ChangeSkinPackPreview(SkinPackType newSkinPack)
        {
            if(SkinPackPreview == newSkinPack)
                return;
            
            SkinPackPreview = newSkinPack;
            OnSkinPackPreviewChanged?.Invoke();
        }
    }
}