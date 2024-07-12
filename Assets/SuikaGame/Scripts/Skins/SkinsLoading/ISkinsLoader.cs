using System;
using SuikaGame.Scripts.Skins.Entities;
using UnityEngine;

namespace SuikaGame.Scripts.Skins.SkinsLoading
{
    public interface ISkinsLoader
    {
        public EntitiesSkinPackConfig EntitiesSkinPackConfig { get; }
        public Sprite BackgroundSkin { get; }

        public event Action OnEntitiesSkinsLoaded;
        public event Action OnBackgroundSkinLoaded;
    }
}