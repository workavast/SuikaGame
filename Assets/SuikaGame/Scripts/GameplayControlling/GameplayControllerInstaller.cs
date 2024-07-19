using System;
using Zenject;

namespace SuikaGame.Scripts.GameplayControlling
{
    public class GameplayControllerInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            var gameplayController = FindObjectOfType<GameplayController>();
            if (gameplayController == null)
                throw new NullReferenceException($"gameplayController is null");

            Container.BindInterfacesTo<GameplayController>().FromInstance(gameplayController).AsSingle();
        }
    }
}