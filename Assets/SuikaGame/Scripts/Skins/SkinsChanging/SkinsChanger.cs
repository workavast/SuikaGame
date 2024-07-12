using System;
using SuikaGame.Scripts.Saves.SkinsPacks;
using SuikaGame.Scripts.Skins.Backgrounds;
using SuikaGame.Scripts.Skins.Entities;

namespace SuikaGame.Scripts.Skins.SkinsChanging
{
    public class SkinsChanger : ISkinsChanger
    {
        private readonly SkinsPacksSettings _skinsPacksSettings;
        
        public EntitiesSkinPackType ActiveEntitiesSkinPack => _skinsPacksSettings.ActiveEntitiesSkinPack;
        public BackgroundSkinType ActiveBackgroundSkin => _skinsPacksSettings.ActiveBackgroundSkin;

        public event Action OnActiveEntitySkinChanged;
        public event Action OnActiveBackgroundChanged;

        public SkinsChanger(SkinsPacksSettings skinsPacksSettings)
        {
            _skinsPacksSettings = skinsPacksSettings;
        }

        public void ChangeActiveEntitySkin(EntitiesSkinPackType newSkin)
        {
            if(ActiveEntitiesSkinPack == newSkin)
                return;
            
            _skinsPacksSettings.SetActiveSkinPack(newSkin);
            OnActiveEntitySkinChanged?.Invoke();
        }

        public void ChangeActiveBackgroundSkin(BackgroundSkinType newSkin)
        {
            if(ActiveBackgroundSkin == newSkin)
                return;
            
            _skinsPacksSettings.SetActiveBackgroundSkin(newSkin);
            OnActiveBackgroundChanged?.Invoke();
        }
    }
}