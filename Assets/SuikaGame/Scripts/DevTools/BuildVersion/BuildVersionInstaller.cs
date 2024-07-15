using UnityEngine;
using Zenject;

namespace SuikaGame.Scripts.DevTools.BuildVersion
{
    public class BuildVersionInstaller : MonoInstaller
    {
        [SerializeField] private bool developmentBuild = true;
        [SerializeField] private BuildVersionView viewPrefab;

        public override void InstallBindings()
        {
            if(!developmentBuild)
                return;
                    
            Instantiate(viewPrefab);
        }
    }
}