using System;
using SuikaGame.Scripts.Entities.Factory;
using SuikaGame.Scripts.EntityMaxSizeCounting;
using UnityEngine;
using Zenject;

namespace SuikaGame.Scripts.Entities
{
    public sealed class EntitiesInstaller : MonoInstaller
    {
        [SerializeField] private EntitiesConfig entitiesConfig;

        public override void InstallBindings()
        {
            BindConfig();
            BindRepository();
            BindFactory();
            BindEntityMaxSizeCounter();
        }
        
        private void BindConfig()
        {
            if (entitiesConfig == null)
                throw new NullReferenceException($"entitiesConfig is null");
            
            Container.BindInstance(entitiesConfig).AsSingle();
        }

        private void BindRepository()
        {
            Container.BindInterfacesTo<EntitiesRepository>().FromNew().AsSingle();
        }
        
        private void BindFactory()
        {
            var factory = FindObjectOfType<EntityFactory>();
            if (factory == null)
                throw new NullReferenceException($"factory is null");

            Container.BindInterfacesTo<EntityFactory>().FromInstance(factory).AsSingle();
        }
        
        private void BindEntityMaxSizeCounter()
        {
            Container.BindInterfacesTo<EntityMaxSizeCounter>().FromNew().AsSingle().NonLazy();
        }
    }
}