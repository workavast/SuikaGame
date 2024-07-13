using System;
using SuikaGame.Scripts.Skins;
using SuikaGame.Scripts.Skins.Backgrounds;
using SuikaGame.Scripts.Skins.Entities;

namespace SuikaGame.Scripts.Saves.SkinsPacks
{
    [Serializable]
    public class SkinsSettingsSave
    {
        public EntitiesSkinPackType activeEntitiesSkinPack = EntitiesSkinPackType.Fruits;
        public BackgroundSkinType activeBackgroundSkin = BackgroundSkinType.Fruits;

        public SkinsSettingsSave()
        {
            activeEntitiesSkinPack = EntitiesSkinPackType.Fruits;
            activeBackgroundSkin = BackgroundSkinType.Fruits;
        }
        
        public SkinsSettingsSave(SkinsSettings settings)
        {
            activeEntitiesSkinPack = settings.ActiveEntitiesSkinPack;
            activeBackgroundSkin = settings.ActiveBackgroundSkin;
        }
    }
}