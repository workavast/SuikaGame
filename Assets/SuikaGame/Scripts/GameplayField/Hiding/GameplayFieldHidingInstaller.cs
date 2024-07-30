using UnityEngine;
using Zenject;

namespace SuikaGame.Scripts.GameplayField.Hiding
{
    public class GameplayFieldHidingInstaller : MonoInstaller
    {
        [SerializeField] private GameplayFieldVfxHider gameplayFieldVfxHider;
        
        public override void InstallBindings()
        {
            Container.BindInstance(gameplayFieldVfxHider).AsSingle();
        }
    }
}