using System;
using System.Collections.Generic;
using Avastrad.EnumValuesLibrary;
using SuikaGame.Scripts.Skins;
using SuikaGame.Scripts.Skins.Backgrounds;
using SuikaGame.Scripts.Skins.Entities;

namespace SuikaGame.Scripts.Saves.SkinsPacks
{
    public class SkinsSettings : ISettings, ISkinsChanger
    {
        private readonly Dictionary<EntitiesSkinPackType, bool> _availableEntitiesSkinPacks = new();
        private readonly Dictionary<BackgroundSkinType, bool> _availableBackgroundSkins = new();
        
        public EntitiesSkinPackType EquippedEntitiesSkinPack { get; private set; }
        public BackgroundSkinType EquippedBackgroundSkin { get; private set; }
        public bool IsEntitiesSkinPackInitialized { get; private set; }
        public bool IsBackgroundSkinInitialized { get; private set; }
        public IReadOnlyDictionary<EntitiesSkinPackType, bool> AvailableEntitiesSkinPacks => _availableEntitiesSkinPacks;
        public IReadOnlyDictionary<BackgroundSkinType, bool> AvailableBackgroundSkins => _availableBackgroundSkins;
        
        public event Action OnChange;
        public event Action OnEntitiesSkinPackEquipped;
        public event Action OnBackgroundSkinEquipped;
        public event Action OnBackgroundSkinUnlocked;
        public event Action OnEntitiesSkinPackUnlocked;

        public SkinsSettings()
        {
            EquippedEntitiesSkinPack = EntitiesSkinPackType.Fruits;
            EquippedBackgroundSkin = BackgroundSkinType.Fruits;

            var entitiesSkinPackTypes = EnumValuesTool.GetValues<EntitiesSkinPackType>();
            foreach (var entitiesSkinPackType in entitiesSkinPackTypes)
                _availableEntitiesSkinPacks.Add(entitiesSkinPackType, false);
            _availableEntitiesSkinPacks[EntitiesSkinPackType.Fruits] = true;
            
            var backgroundSkinTypes = EnumValuesTool.GetValues<BackgroundSkinType>();
            foreach (var backgroundSkinType in backgroundSkinTypes)
                _availableBackgroundSkins.Add(backgroundSkinType, false);
            _availableBackgroundSkins[BackgroundSkinType.Fruits] = true;
        }
        
        public void EquipSkin(EntitiesSkinPackType newSkin)
        {
            if(EquippedEntitiesSkinPack == newSkin)
                return;
            
            EquippedEntitiesSkinPack = newSkin;
            OnChange?.Invoke();
            OnEntitiesSkinPackEquipped?.Invoke();
        }

        public void EquipSkin(BackgroundSkinType newSkin)
        { 
            if(EquippedBackgroundSkin == newSkin)
                return;
            
            EquippedBackgroundSkin = newSkin;
            OnChange?.Invoke();
            OnBackgroundSkinEquipped?.Invoke();
        }

        public void UnlockSkin(EntitiesSkinPackType skin)
        {
            if (_availableEntitiesSkinPacks[skin])
                return;

            _availableEntitiesSkinPacks[skin] = true;
            OnEntitiesSkinPackUnlocked?.Invoke();
        }

        public void UnlockSkin(BackgroundSkinType skin)
        {
            if (_availableBackgroundSkins[skin])
                return;

            _availableBackgroundSkins[skin] = true;
            OnBackgroundSkinUnlocked?.Invoke();
        }

        public void LoadData(SkinsSettingsSave save)
        {
            var prevEntitiesSkinPack = EquippedEntitiesSkinPack;
            var prevBackgroundSkin = EquippedBackgroundSkin;
            
            EquippedEntitiesSkinPack = save.activeEntitiesSkinPack;
            EquippedBackgroundSkin = save.activeBackgroundSkin;
            
            if(prevEntitiesSkinPack != EquippedEntitiesSkinPack || !IsEntitiesSkinPackInitialized)
                OnEntitiesSkinPackEquipped?.Invoke();
            if(prevBackgroundSkin != EquippedBackgroundSkin || !IsBackgroundSkinInitialized)
                OnBackgroundSkinEquipped?.Invoke();
            
            IsEntitiesSkinPackInitialized = true;
            IsBackgroundSkinInitialized = true;
            
            foreach (var skinPack in save.AvailableEntitiesSkinPacks)
                _availableEntitiesSkinPacks[skinPack.Key] = skinPack.IsAvailable;
            
            foreach (var backgroundSkin in save.AvailableBackgroundSkins)
                _availableBackgroundSkins[backgroundSkin.Key] = backgroundSkin.IsAvailable;
        }
    }
}