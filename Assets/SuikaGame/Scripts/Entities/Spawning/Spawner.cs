using Avastrad.CustomTimer;
using Avastrad.EventBusFramework;
using SuikaGame.Scripts.Entities.Factory;
using SuikaGame.Scripts.EntityMaxSizeCounting;
using UnityEngine;
using Zenject;

namespace SuikaGame.Scripts.Entities
{
    public class Spawner : MonoBehaviour, IEventReceiver<EntityCollisionEvent>
    {
        [SerializeField] private SpawnerConfig spawnerConfig;

        public EventBusReceiverIdentifier EventBusReceiverIdentifier { get; } = new();

        private IEventBus _eventBus;
        private IEntityFactory _entityFactory;
        private IEntityMaxSizeCounter _entityMaxSizeCounter;
        private EntitiesConfig _entitiesConfig;
        private Timer _pauseTimer;
        private Entity _currentEntity;

        [Inject]
        public void Construct(IEventBus eventBus, IEntityFactory entityFactory,
            IEntityMaxSizeCounter entityMaxSizeCounter, EntitiesConfig entitiesConfig)
        {
            _eventBus = eventBus;
            _entityFactory = entityFactory;
            _entitiesConfig = entitiesConfig;
            _entityMaxSizeCounter = entityMaxSizeCounter;

            _eventBus.Subscribe(this);
            
            _pauseTimer = new Timer(spawnerConfig.PauseTime, spawnerConfig.PauseTime);
            _pauseTimer.OnTimerEnd += SpawnEntity;
        }
        
        private void Start() 
            => SpawnEntity();

        private void Update() 
            => _pauseTimer.Tick(Time.deltaTime);

        public void DropEntity()
        {
            if (!_pauseTimer.TimerIsEnd)
                return;

            _pauseTimer.Reset();
            _currentEntity.Activate();
            _currentEntity = null;
        }

        public void MoveEntity(Vector2 pos)
        {
            if (_currentEntity == null)
                return;

            pos.y = transform.position.y;
            pos.x = Mathf.Clamp(pos.x, transform.position.x - spawnerConfig.Range,
                transform.position.x + spawnerConfig.Range);
            _currentEntity.transform.position = pos;
        }

        public void OnEvent(EntityCollisionEvent t)
        {
            if (t.Parent.SizeIndex >= _entitiesConfig.Prefabs.Count - 1)
                return;

            var sizeIndex = 1 + t.Parent.SizeIndex;
            var childPos = t.Child.transform.position;

            t.Parent.ManualReturnInPool();
            t.Child.ManualReturnInPool();

            _entityFactory.Create(sizeIndex, childPos);
        }

        private void SpawnEntity()
        {
            var sizeIndex = spawnerConfig.GetSizeIndex(_entityMaxSizeCounter.CurrentMaxSize);
            _currentEntity = _entityFactory.Create(sizeIndex, transform.position);
            _currentEntity.DeActivate();
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