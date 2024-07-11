using UnityEngine;
using Zenject;

namespace SuikaGame.Scripts.Skins.BackgroundSkinUpdating
{
    public class BackgroundSkinUpdaterInstaller : MonoInstaller
    {
        [SerializeField] private BackgroundHolder backgroundHolder;

        public override void InstallBindings()
        {
            Container.BindInterfacesAndSelfTo<BackgroundSkinUpdater>().FromNew().AsSingle().WithArguments(backgroundHolder).NonLazy();
        }
    }
}