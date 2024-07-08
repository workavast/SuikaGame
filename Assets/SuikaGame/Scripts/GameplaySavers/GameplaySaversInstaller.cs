using System;
using SuikaGame.Scripts.GameplaySavers.AutoSaver;
using SuikaGame.Scripts.GameplaySavers.ManualSaver;
using UnityEngine;
using Zenject;

namespace SuikaGame.Scripts.GameplaySavers
{
    public class GameplaySaversInstaller : MonoInstaller
    {
        [SerializeField] private AutoGameplaySaverConfig autoGameplaySaverConfig;
        [SerializeField] private ManualGameplaySaverConfig manualGameplaySaverConfig;
        
        
        public override void InstallBindings()
        {
            if (autoGameplaySaverConfig == null || manualGameplaySaverConfig == null)
                throw new NullReferenceException("autoGameplaySaverConfig == null || manualGameplaySaverConfig == null");
            
            BindGameplaySaver();
            BindAutoGameplaySaver();
            BindManualGameplaySaver();
            BindApplicationFocusSaver();
        }

        private void BindGameplaySaver()
        {
            Container.BindInterfacesTo<Saver>().FromNew().AsSingle().NonLazy();
        }
        
        private void BindAutoGameplaySaver()
        {
            Container.BindInterfacesTo<AutoGameplaySaver>().FromNew().AsSingle().WithArguments(autoGameplaySaverConfig).NonLazy();
        }
        
        private void BindManualGameplaySaver()
        {
            Container.BindInterfacesTo<ManualGameplaySaver>().FromNew().AsSingle().WithArguments(manualGameplaySaverConfig).NonLazy();
        }

        private void BindApplicationFocusSaver()
        {
            var applicationFocusSaver = new GameObject
            {
                name = "ApplicationFocusSaver",
            };

            applicationFocusSaver.AddComponent<ApplicationFocusSaver>();
        }
    }
}