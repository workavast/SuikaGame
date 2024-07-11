using System;
using SuikaGame.Scripts.Skins.SkinPackLoading;

namespace SuikaGame.Scripts.Skins.BackgroundSkinUpdating
{
    public class BackgroundSkinUpdater : IDisposable
    {
        private readonly ISkinPackLoader _skinPackLoader;
        private readonly BackgroundHolder _backgroundHolder;

        public BackgroundSkinUpdater(ISkinPackLoader skinPackLoader, BackgroundHolder backgroundHolder)
        {
            _skinPackLoader = skinPackLoader;
            _backgroundHolder = backgroundHolder;

            _skinPackLoader.OnSkinPackLoaded += SetSkinPack;
        }

        private void SetSkinPack() 
            => _backgroundHolder.ChangeSkin(_skinPackLoader.PackConfig.Background);

        public void Dispose()
        {
            _skinPackLoader.OnSkinPackLoaded -= SetSkinPack;
        }
    }
}