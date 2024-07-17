using System;
using System.Collections.Generic;
using Avastrad.EnumValuesLibrary;
using SuikaGame.Scripts.Skins.Backgrounds;
using SuikaGame.Scripts.Skins.Entities;

namespace SuikaGame.Scripts.Saves.SkinsPacks
{
    [Serializable]
    public class SkinsSettingsSave
    {
        public EntitiesSkinPackType activeEntitiesSkinPack = EntitiesSkinPackType.Fruits;
        public BackgroundSkinType activeBackgroundSkin = BackgroundSkinType.Fruits;

        public List<AvailableCellSave<EntitiesSkinPackType>> AvailableEntitiesSkinPacks = new();
        public List<AvailableCellSave<BackgroundSkinType>> AvailableBackgroundSkins = new();
        
        public SkinsSettingsSave()
        {
            activeEntitiesSkinPack = EntitiesSkinPackType.Fruits;
            activeBackgroundSkin = BackgroundSkinType.Fruits;

            var entitiesSkinPackTypes = EnumValuesTool.GetValues<EntitiesSkinPackType>();
            foreach (var entitiesSkinPackType in entitiesSkinPackTypes)
                AvailableEntitiesSkinPacks.Add(new AvailableCellSave<EntitiesSkinPackType>(entitiesSkinPackType));
            AvailableEntitiesSkinPacks.Find(save => save.Key == EntitiesSkinPackType.Fruits).IsAvailable = true;
            
            var backgroundSkinTypes = EnumValuesTool.GetValues<BackgroundSkinType>();
            foreach (var backgroundSkinType in backgroundSkinTypes)
                AvailableBackgroundSkins.Add(new AvailableCellSave<BackgroundSkinType>(backgroundSkinType));
            AvailableBackgroundSkins.Find(save => save.Key == BackgroundSkinType.Fruits).IsAvailable = true;
        }
        
        public SkinsSettingsSave(SkinsSettings settings)
        {
            activeEntitiesSkinPack = settings.EquippedEntitiesSkinPack;
            activeBackgroundSkin = settings.EquippedBackgroundSkin;
            
            foreach (var keyValuePair in settings.AvailableEntitiesSkinPacks)
                AvailableEntitiesSkinPacks.Add(new AvailableCellSave<EntitiesSkinPackType>(keyValuePair));
            foreach (var keyValuePair in settings.AvailableBackgroundSkins)
                AvailableBackgroundSkins.Add(new AvailableCellSave<BackgroundSkinType>(keyValuePair));
        }
    }
}