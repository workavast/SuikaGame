using SuikaGame.Scripts.Saves;
using Zenject;

namespace SuikaGame.Scripts.GameplayField.Model
{
    public class GameplayFieldModelInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            Container.BindInterfacesAndSelfTo<IGameplayFieldModel>()
                .FromInstance(PlayerData.Instance.GameplaySceneSettings).AsSingle();
        }
    }
}