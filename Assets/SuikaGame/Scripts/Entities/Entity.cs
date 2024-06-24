using System;
using Avastrad.EventBusFramework;
using UnityEngine;
using Zenject;

namespace SuikaGame.Scripts.Entities
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class Entity : MonoBehaviour, Avastrad.PoolSystem.IPoolable<Entity, int>
    {
        public int PoolId => SizeIndex;
        public int Index { get; private set; }
        public int SizeIndex { get; private set; }
        
        private static int _index;
        private IEventBus _eventBus;
        private Rigidbody2D _rigidbody2D;
        
        public event Action<Entity> ReturnElementEvent;
        public event Action<Entity> DestroyElementEvent;
        
        [Inject]
        public void Construct(IEventBus eventBus)
        {
            _eventBus = eventBus;
        }
        
        private void Awake()
        {
            Index = _index++;
            _rigidbody2D = GetComponent<Rigidbody2D>();
            _rigidbody2D.mass = transform.localScale.x;
        }

        public void Initialize(int sizeIndex) 
            => SizeIndex = sizeIndex;

        public void Activate()
        {
            _rigidbody2D.simulated = true;
        }

        public void DeActivate()
        {
            _rigidbody2D.simulated = false;
        }
        
        public void Return() 
            => ReturnElementEvent?.Invoke(this);
        
        public void OnElementExtractFromPool()
        {
            gameObject.SetActive(true);
        }

        public void OnElementReturnInPool()
        {
            gameObject.SetActive(false);
        }
        
        private void OnCollisionEnter2D(Collision2D other)
        {
            if (other.gameObject.TryGetComponent(out Entity entity))
            {
                if (Index > entity.Index && SizeIndex == entity.SizeIndex)
                {
                    _eventBus.Invoke(new EntityCollisionEvent(this, entity));
                }
            }
        }
        
        private void OnDestroy()
        {
            DestroyElementEvent?.Invoke(this);
        }
    }
}
