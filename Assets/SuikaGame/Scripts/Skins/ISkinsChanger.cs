using System;
using System.Collections.Generic;
using SuikaGame.Scripts.Skins.Backgrounds;
using SuikaGame.Scripts.Skins.Entities;

namespace SuikaGame.Scripts.Skins
{
    public interface ISkinsChanger
    {
        public EntitiesSkinPackType EquippedEntitiesSkinPack { get; }
        public BackgroundSkinType EquippedBackgroundSkin { get; }
        public bool IsEntitiesSkinPackInitialized { get; }
        public bool IsBackgroundSkinInitialized { get; }
        public IReadOnlyDictionary<EntitiesSkinPackType, bool> AvailableEntitiesSkinPacks { get; }
        public IReadOnlyDictionary<BackgroundSkinType, bool> AvailableBackgroundSkins { get; }
        
        public event Action OnEntitiesSkinPackEquipped;
        public event Action OnBackgroundSkinEquipped;
        public event Action OnBackgroundSkinUnlocked;
        public event Action OnEntitiesSkinPackUnlocked;
        

        public void EquipSkin(EntitiesSkinPackType newSkin);
        public void EquipSkin(BackgroundSkinType newSkin);

        public void UnlockSkin(EntitiesSkinPackType skin);
        public void UnlockSkin(BackgroundSkinType skin);
    }
}