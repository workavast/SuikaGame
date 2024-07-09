using System;
using SuikaGame.Scripts.Skins;

namespace SuikaGame.Scripts.Saves.SkinsPacks
{
    [Serializable]
    public class SkinsPacksSettingsSave
    {
        public SkinPackType ActiveSkinPack = SkinPackType.Fruits;

        public SkinsPacksSettingsSave()
        {
            ActiveSkinPack = SkinPackType.Fruits;
        }
        
        public SkinsPacksSettingsSave(SkinsPacksSettings settings)
        {
            ActiveSkinPack = settings.ActiveSkinPack;
        }
    }
}