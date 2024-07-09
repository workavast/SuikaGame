using System;
using SuikaGame.Scripts.Saves.SkinsPacks;

namespace SuikaGame.Scripts.Skins
{
    public class SkinPackChanger : ISkinPackChanger
    {
        private readonly SkinsPacksSettings _skinsPacksSettings;
        
        public SkinPackType ActiveSkinPack => _skinsPacksSettings.ActiveSkinPack;

        public event Action OnActiveSkinPackChanged;
        
        public SkinPackChanger(SkinsPacksSettings skinsPacksSettings)
        {
            _skinsPacksSettings = skinsPacksSettings;
        }

        public void ChangeActiveSkinPack(SkinPackType newSkinPack)
        {
            if(ActiveSkinPack == newSkinPack)
                return;
            
            _skinsPacksSettings.SetActiveSkinPack(newSkinPack);
            OnActiveSkinPackChanged?.Invoke();
        }
    }
}