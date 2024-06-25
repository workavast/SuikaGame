using System;
using Zenject;

namespace SuikaGame.Scripts.GameControlling
{
    public class GameControllerInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            var gameController = FindObjectOfType<GameController>();
            if (gameController == null)
                throw new NullReferenceException($"gameController is null");

            Container.BindInterfacesTo<GameController>().FromInstance(gameController).AsSingle();
        }
    }
}