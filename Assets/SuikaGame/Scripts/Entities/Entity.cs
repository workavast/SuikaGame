using System;
using Avastrad.EventBusFramework;
using SuikaGame.Scripts.SerializedTypes;
using SuikaGame.Scripts.Vfx;
using UnityEngine;
using Zenject;

namespace SuikaGame.Scripts.Entities
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class Entity : MonoBehaviour, Avastrad.PoolSystem.IPoolable<Entity, int>
    {
        public int PoolId => SizeIndex;
        public bool IsActive { get; private set; } = true;
        public int CreateIndex { get; private set; }
        public int SizeIndex { get; private set; }
        public Vector2 Velocity => _rigidbody2D.velocity;
        public float Mass => _rigidbody2D.mass;
        
        private static int _globalCreateIndex;
        private IEventBus _eventBus;
        private IVfxFactory _vfxFactory;
        private Rigidbody2D _rigidbody2D;
        private CircleCollider2D _circleCollider2D;
        
        public event Action<Entity> ReturnElementEvent;
        public event Action<Entity> DestroyElementEvent;
        
        [Inject]
        public void Construct(IEventBus eventBus, IVfxFactory vfxFactory)
        {
            _eventBus = eventBus;
            _vfxFactory = vfxFactory;
        }
        
        private void Awake()
        {
            CreateIndex = _globalCreateIndex++;
            _rigidbody2D = GetComponent<Rigidbody2D>();
            _circleCollider2D = GetComponent<CircleCollider2D>();
            
            _rigidbody2D.mass = transform.localScale.x;
        }

        public void Initialize(int sizeIndex) 
            => SizeIndex = sizeIndex;

        public void SetVelocity(Vector2 velocity) 
            => _rigidbody2D.velocity = velocity;

        public void Activate()
        {
            IsActive = true;
            _circleCollider2D.enabled = true;
            _rigidbody2D.simulated = true;
        }

        public void DeActivate()
        {
            IsActive = false;
            _circleCollider2D.enabled = false;
            _rigidbody2D.simulated = false;
        }

        public EntityModel GetModel()
            => new EntityModel(this);
        
        public void ManualReturnInPool()
        {
            _vfxFactory.Create(VfxType.Smoke, transform.position, transform.localScale.x);
            ReturnElementEvent?.Invoke(this);
        }

        public void InstantlyReturnInPool() 
            => ReturnElementEvent?.Invoke(this);

        public void OnElementExtractFromPool() 
            => gameObject.SetActive(true);

        public void OnElementReturnInPool() 
            => gameObject.SetActive(false);

        private void OnCollisionEnter2D(Collision2D other)
        {
            if (other.gameObject.TryGetComponent(out Entity entity))
                if (CreateIndex > entity.CreateIndex && SizeIndex == entity.SizeIndex)
                    _eventBus.Invoke(new EntityCollisionEvent(this, entity));
        }
        
        private void OnDestroy() 
            => DestroyElementEvent?.Invoke(this);
    }
}