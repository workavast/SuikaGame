using Zenject;

namespace SuikaGame.Scripts.Skins.EntitiesSkinsUpdating
{
    public class EntitiesSkinsUpdaterInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            Container.BindInterfacesAndSelfTo<EntitiesSkinsUpdater>().FromNew().AsSingle().NonLazy();
        }
    }
}