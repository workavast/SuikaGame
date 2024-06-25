using System;
using Avastrad.CustomTimer;
using Avastrad.PoolSystem;
using UnityEngine;

namespace SuikaGame.Scripts.Vfx
{
    public class VfxHolder : MonoBehaviour, IPoolable<VfxHolder, VfxType>
    {
        [field: SerializeField] public VfxType PoolId { get; private set; }
        [SerializeField] private ParticleSystem particleSystem;
        
        public event Action<VfxHolder> ReturnElementEvent;
        public event Action<VfxHolder> DestroyElementEvent;

        private Timer _durationTimer;
        
        private void Awake()
        {
            _durationTimer = new Timer(particleSystem.main.duration);
            _durationTimer.OnTimerEnd += () => ReturnElementEvent?.Invoke(this);
        }

        public void Update()
        {
            _durationTimer.Tick(Time.deltaTime);
        }

        public void OnElementExtractFromPool()
        {
            gameObject.SetActive(true);
            particleSystem.Play();
            _durationTimer.Reset();
        }

        public void OnElementReturnInPool()
        {
            particleSystem.Stop();
            _durationTimer.SetPause();
            gameObject.SetActive(false);
        }

        private void OnDestroy() 
            => DestroyElementEvent?.Invoke(this);
    }
}