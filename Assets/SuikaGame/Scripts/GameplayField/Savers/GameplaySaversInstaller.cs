using System;
using SuikaGame.Scripts.GameplayField.Savers.AutoSaver;
using SuikaGame.Scripts.GameplayField.Savers.ManualSaver;
using UnityEngine;
using Zenject;

namespace SuikaGame.Scripts.GameplayField.Savers
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
            BindApplicationPauseSaver();
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
            Container.BindInterfacesTo<ApplicationFocusSaver>().FromNew().AsSingle().NonLazy();
        }
        
        private void BindApplicationPauseSaver()
        {
            Container.BindInterfacesTo<ApplicationPauseSaver>().FromNew().AsSingle().NonLazy();
        }
    }
}