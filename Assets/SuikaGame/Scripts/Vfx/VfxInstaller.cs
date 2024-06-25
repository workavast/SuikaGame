using System;
using UnityEngine;
using Zenject;

namespace SuikaGame.Scripts.Vfx
{
    public class VfxInstaller : MonoInstaller
    {
        [SerializeField] private VfxConfig vfxConfig;
        
        public override void InstallBindings()
        {
            BindConfig();
            BindFactory();
        }

        private void BindConfig()
        {
            if (vfxConfig == null)
                throw new NullReferenceException($"vfxConfig is null");

            Container.BindInstance(vfxConfig).AsSingle();
        }

        private void BindFactory()
        {
            var factory = FindObjectOfType<VfxFactory>();
            if (factory == null)
                throw new NullReferenceException($"factory is null");

            Container.BindInterfacesTo<VfxFactory>().FromInstance(factory).AsSingle();
        }
    }
}