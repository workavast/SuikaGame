using System;
using Avastrad.CustomTimer;
using Avastrad.EventBusFramework;
using SuikaGame.Scripts.Entities.Factory;
using SuikaGame.Scripts.Entities.MaxSizeCounting;
using SuikaGame.Scripts.InputDetection;
using UnityEngine;
using Zenject;

namespace SuikaGame.Scripts.Entities.Spawning
{
    public class EntitySpawner : MonoBehaviour, IEntitySpawner, INextEntitySpawner, IEventReceiver<EntityCollisionEvent>
    {
        [SerializeField] private StartEntityVelocityConfig startEntityVelocityConfig;
        [SerializeField] private SpawnerConfig spawnerConfig;

        public EventBusReceiverIdentifier EventBusReceiverIdentifier { get; } = new();
        public int NextEntitySizeIndex { get; private set; }

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
        public event Action<int> OnNextEntitySizeIndexChange;
        
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

            _pauseTimer = new Timer(spawnerConfig.PauseTime, spawnerConfig.PauseTime, true);
            _pauseTimer.OnTimerEnd += SpawnEntity;

            _input.Pressed += MoveEntity;
            _input.Hold += MoveEntity;
            _input.Release += DropEntity;
        }
        
        public void Initialize()
        {
            _pauseTimer.Reset();
            _pauseTimer.SetCurrentTime(_pauseTimer.MaxTime);
            SpawnEntity();
        }

        private void Update() 
            => _pauseTimer.Tick(Time.deltaTime);

        public void Reset()
        {
            _pauseTimer.SetPause();
            _currentEntity = null;
        }

        public void OnEvent(EntityCollisionEvent t)
        {
            if (t.Parent.SizeIndex >= _entitiesConfig.Prefabs.Count - 1)
                return;

            if (!t.Parent.IsActive || !t.Child.IsActive)
                return;

            var sizeIndex = 1 + t.Parent.SizeIndex;
            var childPos = t.Child.transform.position;

            var mass = t.Parent.Mass;
            var parentVelocity = t.Parent.Velocity;
            var childVelocity = t.Child.Velocity;
            
            t.Parent.DeActivate();
            t.Child.DeActivate();
            t.Parent.ManualReturnInPool();
            t.Child.ManualReturnInPool();

            var entity = _entityFactory.Create(sizeIndex, childPos);
            var newVelocity = (parentVelocity + childVelocity) * mass / entity.Mass;
            entity.Activate();
            entity.SetVelocity(newVelocity);
            _eventBus.Invoke(new MergeEvent(sizeIndex));
        }

        private void SpawnEntity()
        {
            if (_currentEntity != null)
                return;
            
            _currentEntity = _entityFactory.Create(NextEntitySizeIndex, transform.position);
            NextEntitySizeIndex = spawnerConfig.GetSizeIndex(_entityMaxSizeCounter.CurrentMaxSize);
            OnNextEntitySizeIndexChange?.Invoke(NextEntitySizeIndex);
            _currentEntity.DeActivate();
            OnSpawnEntity?.Invoke();
            
            if (_input.IsHold)
                MoveEntity(_input.HoldPoint);
            else
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
                Gizmos.DrawWireCube(transform.position, Vector3.up * 0.1f + Vector3.right * spawnerConfig.Range * 2);
        }
    }
}