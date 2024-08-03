using Zenject;

namespace SuikaGame.Scripts.GamePausing
{
    public class GameInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            BindPauser();
            BindGameplayWebStateSwitcher();
        }

        private void BindPauser()
        {
            Container.BindInterfacesTo<GamePushPauser>().FromNew().AsSingle().NonLazy();
        }
        
        private void BindGameplayWebStateSwitcher()
        {
            Container.Bind<GameplayWebStateSwitcher>().FromNew().AsSingle().NonLazy();
        }
    }
}