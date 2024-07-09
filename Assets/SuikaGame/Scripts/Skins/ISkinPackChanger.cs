using System;

namespace SuikaGame.Scripts.Skins
{
    public interface ISkinPackChanger
    {
        public SkinPackType ActiveSkinPack { get; }

        public event Action OnActiveSkinPackChanged;

        public void ChangeActiveSkinPack(SkinPackType newSkinPack);
    }
}