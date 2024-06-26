using System;
using Avastrad.CustomTimer;
using Avastrad.EventBusFramework;
using SuikaGame.Scripts.Entities.Factory;
using SuikaGame.Scripts.EntityMaxSizeCounting;
using SuikaGame.Scripts.InputDetection;
using UnityEngine;
using Zenject;

namespace SuikaGame.Scripts.Entities.Spawning
{
    public class EntitySpawner : MonoBehaviour, IEntitySpawner, IEventReceiver<EntityCollisionEvent>
    {
        [SerializeField] private StartEntityVelocityConfig startEntityVelocityConfig;
        [SerializeField] private SpawnerConfig spawnerConfig;

        public EventBusReceiverIdentifier EventBusReceiverIdentifier { get; } = new();

        private IInput _input;
        private IEventBus _eventBus;
        private IEntityFactory _entityFactory;
        private IEntityMaxSizeCounter _entityMaxSizeCounter;
        private EntitiesConfig _entitiesConfig;
        private Timer _pauseTimer;
        private Entity _currentEntity;

        public event Action OnSpawnEntity;
        public event Action OnDeSpawnEntity;
        public event Action<Vector2> OnMoveEntity;
        
        [Inject]
        public void Construct(IEventBus eventBus, IEntityFactory entityFactory,
            IEntityMaxSizeCounter entityMaxSizeCounter, IInput input, EntitiesConfig entitiesConfig)
        {
            _eventBus = eventBus;
            _entityFactory = entityFactory;
            _entitiesConfig = entitiesConfig;
            _entityMaxSizeCounter = entityMaxSizeCounter;
            _input = input;
            
            _eventBus.Subscribe(this);
            
            _pauseTimer = new Timer(spawnerConfig.PauseTime, spawnerConfig.PauseTime);
            _pauseTimer.OnTimerEnd += SpawnEntity;

            _input.Pressed += MoveEntity;
            _input.Hold += MoveEntity;
            _input.Release += DropEntity;
        }
        
        private void Start() 
            => SpawnEntity();

        private void Update() 
            => _pauseTimer.Tick(Time.deltaTime);

        public void Reset() 
            => SpawnEntity();
        
        public void OnEvent(EntityCollisionEvent t)
        {
            if (t.Parent.SizeIndex >= _entitiesConfig.Prefabs.Count - 1)
                return;

            var sizeIndex = 1 + t.Parent.SizeIndex;
            var childPos = t.Child.transform.position;

            var mass = t.Parent.Mass;
            var parentVelocity = t.Parent.Velocity;
            var childVelocity = t.Child.Velocity;
            
            t.Parent.ManualReturnInPool();
            t.Child.ManualReturnInPool();

            var entity = _entityFactory.Create(sizeIndex, childPos);
            var newVelocity = (parentVelocity + childVelocity) * mass / entity.Mass;
            entity.Activate();
            entity.SetVelocity(newVelocity);
        }

        private void SpawnEntity()
        {
            var sizeIndex = spawnerConfig.GetSizeIndex(_entityMaxSizeCounter.CurrentMaxSize);
            _currentEntity = _entityFactory.Create(sizeIndex, transform.position);
            _currentEntity.DeActivate();
            OnSpawnEntity?.Invoke();
            OnMoveEntity?.Invoke(transform.position);
        }
        
        private void DropEntity(Vector2 point)
        {
            if (!_pauseTimer.TimerIsEnd)
                return;

            _pauseTimer.Reset();
            _currentEntity.Activate();
            _currentEntity.SetVelocity(Vector2.down * startEntityVelocityConfig.StartVelocity);
            _currentEntity = null;
            OnDeSpawnEntity?.Invoke();
        }

        private void MoveEntity(Vector2 pos)
        {
            if (_currentEntity == null)
                return;

            pos.y = transform.position.y;
            var leftClampPoint = transform.position.x - spawnerConfig.Range + _currentEntity.transform.localScale.x / 2;
            var rightClampPoint = transform.position.x + spawnerConfig.Range - _currentEntity.transform.localScale.x / 2;
            pos.x = Mathf.Clamp(pos.x, leftClampPoint, rightClampPoint);
            _currentEntity.transform.position = pos;
            OnMoveEntity?.Invoke(pos);
        }
        private void OnDestroy()
        {
            _eventBus?.UnSubscribe(this);
            _pauseTimer.OnTimerEnd -= SpawnEntity;
        }

        private void OnDrawGizmos()
        {
            if (spawnerConfig != null)
                Gizmos.DrawWireCube(transform.position, Vector3.up + Vector3.right * spawnerConfig.Range * 2);
        }
    }
}