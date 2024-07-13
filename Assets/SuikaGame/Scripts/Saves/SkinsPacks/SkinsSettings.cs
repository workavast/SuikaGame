using System;
using SuikaGame.Scripts.Skins;
using SuikaGame.Scripts.Skins.Backgrounds;
using SuikaGame.Scripts.Skins.Entities;

namespace SuikaGame.Scripts.Saves.SkinsPacks
{
    public class SkinsSettings : ISettings, ISkinsChanger
    {
        public EntitiesSkinPackType ActiveEntitiesSkinPack { get; private set; }
        public BackgroundSkinType ActiveBackgroundSkin { get; private set; }
        public bool IsEntitiesSkinPackInitialized { get; private set; }
        public bool IsBackgroundSkinInitialized { get; private set; }

        public event Action OnChange;
        public event Action OnEntitiesSkinPackChanged;
        public event Action OnBackgroundSkinChanged;
        
        public SkinsSettings()
        {
            ActiveEntitiesSkinPack = EntitiesSkinPackType.Fruits;
            ActiveBackgroundSkin = BackgroundSkinType.Fruits;
        }
        
        public void SetEntitiesSkinPack(EntitiesSkinPackType newSkin)
        {
            if(ActiveEntitiesSkinPack == newSkin)
                return;
            
            ActiveEntitiesSkinPack = newSkin;
            OnChange?.Invoke();
            OnEntitiesSkinPackChanged?.Invoke();
        }

        public void SetBackgroundSkin(BackgroundSkinType newSkin)
        { 
            if(ActiveBackgroundSkin == newSkin)
                return;
            
            ActiveBackgroundSkin = newSkin;
            OnChange?.Invoke();
            OnBackgroundSkinChanged?.Invoke();
        }
        
        public void LoadData(SkinsSettingsSave save)
        {
            var prevEntitiesSkinPack = ActiveEntitiesSkinPack;
            var prevBackgroundSkin = ActiveBackgroundSkin;
            
            ActiveEntitiesSkinPack = save.activeEntitiesSkinPack;
            ActiveBackgroundSkin = save.activeBackgroundSkin;
            
            if(prevEntitiesSkinPack != ActiveEntitiesSkinPack || !IsEntitiesSkinPackInitialized)
                OnEntitiesSkinPackChanged?.Invoke();
            if(prevBackgroundSkin != ActiveBackgroundSkin || !IsBackgroundSkinInitialized)
                OnBackgroundSkinChanged?.Invoke();
            
            IsEntitiesSkinPackInitialized = true;
            IsBackgroundSkinInitialized = true;
        }
    }
}