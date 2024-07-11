using System;

namespace SuikaGame.Scripts.Skins.SkinPackLoading
{
    public interface ISkinPackLoader
    {
        public SkinsPackConfig PackConfig { get; }
        public event Action OnSkinPackLoaded;
    }
}