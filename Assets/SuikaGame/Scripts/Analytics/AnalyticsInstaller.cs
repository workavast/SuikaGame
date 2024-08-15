using SuikaGame.Scripts.Saves;
using Zenject;

namespace SuikaGame.Scripts.Analytics
{
    public class AnalyticsInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            BindProvider();
            BindSettings();
        }

        private void BindProvider()
        {
            Container.BindInterfacesTo<GamePushAnalyticsProvider>().FromNew().AsSingle();
        }

        private void BindSettings()
        {
            Container.BindInstance(PlayerData.Instance.AnalyticsSettings).AsSingle();
        }
    }
}