using System;
using SuikaGame.Scripts.Skins.SkinsLoading;

namespace SuikaGame.Scripts.Skins.Backgrounds.SkinsUpdating
{
    public class BackgroundSkinUpdater : IDisposable
    {
        private readonly ISkinsLoader _skinsLoader;
        private readonly BackgroundHolder _backgroundHolder;

        public BackgroundSkinUpdater(ISkinsLoader skinsLoader, BackgroundHolder backgroundHolder)
        {
            _skinsLoader = skinsLoader;
            _backgroundHolder = backgroundHolder;

            _skinsLoader.OnBackgroundSkinLoaded += SetSkin;
            SetSkin();
        }

        private void SetSkin()
        {
            if(_skinsLoader.BackgroundSkin != null)
                _backgroundHolder.ChangeSkin(_skinsLoader.BackgroundSkin);
        }

        public void Dispose() 
            => _skinsLoader.OnBackgroundSkinLoaded -= SetSkin;
    }
}