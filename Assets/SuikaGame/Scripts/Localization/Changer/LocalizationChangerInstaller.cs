using Zenject;

namespace SuikaGame.Scripts.Localization.Changer
{
    public class LocalizationChangerInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            Container.BindInterfacesAndSelfTo<GamePushLocalizationChanger>().FromNew().AsSingle().NonLazy();
        }
    }
}