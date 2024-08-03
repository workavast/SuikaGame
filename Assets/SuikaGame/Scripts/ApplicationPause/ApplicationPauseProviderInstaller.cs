using UnityEngine;
using Zenject;

namespace SuikaGame.Scripts.ApplicationPause
{
    public class ApplicationPauseProviderInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            var applicationPauseProvider = new GameObject { name = "ApplicationPauseProvider" }
                .AddComponent<ApplicationPauseProvider>();
            
            DontDestroyOnLoad(applicationPauseProvider.gameObject);
            Container.BindInterfacesTo<ApplicationPauseProvider>().FromInstance(applicationPauseProvider).AsSingle();
        }
    }
}