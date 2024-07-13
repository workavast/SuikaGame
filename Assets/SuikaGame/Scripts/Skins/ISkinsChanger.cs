using System;
using SuikaGame.Scripts.Skins.Backgrounds;
using SuikaGame.Scripts.Skins.Entities;

namespace SuikaGame.Scripts.Skins
{
    public interface ISkinsChanger
    {
        public EntitiesSkinPackType ActiveEntitiesSkinPack { get; }
        public BackgroundSkinType ActiveBackgroundSkin { get; }
        public bool IsEntitiesSkinPackInitialized { get; }
        public bool IsBackgroundSkinInitialized { get; }
        
        public event Action OnEntitiesSkinPackChanged;
        public event Action OnBackgroundSkinChanged;

        public void SetEntitiesSkinPack(EntitiesSkinPackType newSkin);
        public void SetBackgroundSkin(BackgroundSkinType newSkin);
    }
}