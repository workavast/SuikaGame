using System;
using SuikaGame.Scripts.UI;
using Zenject;

namespace SuikaGame.Scripts.InputDetection
{
    public class InputInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            var input = FindObjectOfType<InteractableZone>();
            if (input == null)
                throw new NullReferenceException($"input is null");

            Container.BindInterfacesTo<InteractableZone>().FromInstance(input).AsSingle();
        }
    }
}