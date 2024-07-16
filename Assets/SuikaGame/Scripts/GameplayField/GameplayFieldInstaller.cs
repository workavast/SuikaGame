using SuikaGame.Scripts.Saves;
using Zenject;

namespace SuikaGame.Scripts.GameplayField
{
    public class GameplayFieldInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            Container.BindInterfacesAndSelfTo<IGameplayFieldModel>()
                .FromInstance(PlayerData.Instance.GameplaySceneSettings).AsSingle();
        }
    }
}