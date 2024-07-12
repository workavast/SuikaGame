using System;
using SuikaGame.Scripts.Skins;
using SuikaGame.Scripts.Skins.Entities;

namespace SuikaGame.Scripts.Saves.SkinsPacks
{
    [Serializable]
    public class SkinsPacksSettingsSave
    {
        public EntitiesSkinPackType activeEntitiesSkinPack = EntitiesSkinPackType.Fruits;

        public SkinsPacksSettingsSave()
        {
            activeEntitiesSkinPack = EntitiesSkinPackType.Fruits;
        }
        
        public SkinsPacksSettingsSave(SkinsPacksSettings settings)
        {
            activeEntitiesSkinPack = settings.ActiveEntitiesSkinPack;
        }
    }
}