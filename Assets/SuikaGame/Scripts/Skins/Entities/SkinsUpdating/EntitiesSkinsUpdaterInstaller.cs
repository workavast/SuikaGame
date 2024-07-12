using Zenject;

namespace SuikaGame.Scripts.Skins.Entities.SkinsUpdating
{
    public class EntitiesSkinsUpdaterInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            Container.BindInterfacesAndSelfTo<EntitiesSkinsUpdater>().FromNew().AsSingle().NonLazy();
        }
    }
}