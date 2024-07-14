using UnityEngine;
using Zenject;

namespace SuikaGame.Scripts.ApplicationFocus
{
    public class ApplicationFocusProviderInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            var applicationFocusProvider = new GameObject { name = "ApplicationFocusProvider" }
                .AddComponent<ApplicationFocusProvider>();
            
            DontDestroyOnLoad(applicationFocusProvider.gameObject);
            Container.BindInterfacesTo<ApplicationFocusProvider>().FromInstance(applicationFocusProvider).AsSingle();
        }
    }
}