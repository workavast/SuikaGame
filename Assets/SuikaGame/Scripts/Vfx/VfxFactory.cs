using System;
using Avastrad.PoolSystem;
using UnityEngine;
using Zenject;

namespace SuikaGame.Scripts.Vfx
{
    public class VfxFactory : MonoBehaviour, IVfxFactory
    {
        private VfxConfig _config;
        private Pool<VfxHolder, VfxType> _pool;

        public event Action<VfxHolder> OnCreate;

        [Inject]
        public void Construct(VfxConfig config)
        {
            _config = config;
        }
        
        private void Awake()
        {
            _pool = new Pool<VfxHolder, VfxType>(Instantiate);
        }
        
        public VfxHolder Create(VfxType vfxType, Vector2 position, float scale)
        {
            _pool.ExtractElement(vfxType, out var vfx);
            
            vfx.transform.position = position;
            vfx.transform.localScale = Vector3.one * scale;

            OnCreate?.Invoke(vfx);
            
            return vfx;
        }

        private VfxHolder Instantiate(VfxType vfxType)
        {
            if (!_config.Prefabs.ContainsKey(vfxType))
                throw new ArgumentOutOfRangeException($"config doesnt contains {vfxType}");

            var vfx = Instantiate(_config.Prefabs[vfxType], transform);
            return vfx;
        }
    }
}