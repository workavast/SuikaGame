using UnityEngine;
using Zenject;

namespace SuikaGame.Scripts.CoroutineHolding
{
    public class CoroutineHolderInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            var coroutineHolder = (new GameObject { name = "CoroutineHolder" }).AddComponent<CoroutineHolder>();
            coroutineHolder.Initialize();
            
            Container.BindInstance(coroutineHolder).AsSingle();
        }
    }
}