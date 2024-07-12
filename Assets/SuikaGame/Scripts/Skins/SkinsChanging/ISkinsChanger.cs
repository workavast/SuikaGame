using System;
using SuikaGame.Scripts.Skins.Backgrounds;
using SuikaGame.Scripts.Skins.Entities;

namespace SuikaGame.Scripts.Skins.SkinsChanging
{
    public interface ISkinsChanger
    {
        public EntitiesSkinPackType ActiveEntitiesSkinPack { get; }
        public BackgroundSkinType ActiveBackgroundSkin { get; }

        public event Action OnActiveEntitySkinChanged;
        public event Action OnActiveBackgroundChanged;

        public void ChangeActiveEntitySkin(EntitiesSkinPackType newSkin);
        public void ChangeActiveBackgroundSkin(BackgroundSkinType newSkin);
    }
}