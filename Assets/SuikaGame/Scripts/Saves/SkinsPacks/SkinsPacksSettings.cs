using System;
using SuikaGame.Scripts.Skins;
using SuikaGame.Scripts.Skins.Backgrounds;
using SuikaGame.Scripts.Skins.Entities;

namespace SuikaGame.Scripts.Saves.SkinsPacks
{
    public class SkinsPacksSettings : ISettings
    {
        public EntitiesSkinPackType ActiveEntitiesSkinPack { get; private set; }
        public BackgroundSkinType ActiveBackgroundSkin { get; private set; }
        
        public event Action OnChange;
        
        public SkinsPacksSettings()
        {
            ActiveEntitiesSkinPack = EntitiesSkinPackType.Fruits;
        }

        public void SetActiveSkinPack(EntitiesSkinPackType newSkin)
        {
            ActiveEntitiesSkinPack = newSkin;
            OnChange?.Invoke();
        }
        
        public void SetActiveBackgroundSkin(BackgroundSkinType newSkin)
        {
            ActiveBackgroundSkin = newSkin;
            OnChange?.Invoke();
        }
        
        public void LoadData(SkinsPacksSettingsSave save)
        {
            ActiveEntitiesSkinPack = save.activeEntitiesSkinPack;
        }
    }
}