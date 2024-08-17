using UnityEngine;
using Zenject;

namespace SuikaGame.Scripts.DevTools.BuildVersion
{
    public class BuildVersionInstaller : MonoInstaller
    {
        [SerializeField] private TextAsset buildVersionConfiguration;
        [SerializeField] private bool developmentBuild = true;
        [SerializeField] private BuildVersionView viewPrefab;

        public override void InstallBindings()
        {
            var buildVersionHolder = JsonUtility.FromJson<BuildVersionHolder>(buildVersionConfiguration.text);
            Debug.Log($"|| Build VERSION {buildVersionHolder.BuildVersion} || ");
            
            if(!developmentBuild)
                return;
                    
            Instantiate(viewPrefab);
        }
    }
}