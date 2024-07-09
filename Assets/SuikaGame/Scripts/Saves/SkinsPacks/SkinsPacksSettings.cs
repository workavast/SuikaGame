using System;
using SuikaGame.Scripts.Skins;

namespace SuikaGame.Scripts.Saves.SkinsPacks
{
    public class SkinsPacksSettings : ISettings
    {
        public SkinPackType ActiveSkinPack { get; private set; }
        
        public event Action OnChange;
        
        public SkinsPacksSettings()
        {
            ActiveSkinPack = SkinPackType.Fruits;
        }

        public void SetActiveSkinPack(SkinPackType newActiveSkinPack)
        {
            ActiveSkinPack = newActiveSkinPack;
            OnChange?.Invoke();
        }
        
        public void LoadData(SkinsPacksSettingsSave save)
        {
            ActiveSkinPack = save.ActiveSkinPack;
        }
    }
}