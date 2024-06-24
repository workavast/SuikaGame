using Avastrad.CustomTimer;
using Avastrad.EventBusFramework;
using SuikaGame.Scripts.Entities.Factory;
using UnityEngine;
using Zenject;
using Random = UnityEngine.Random;

namespace SuikaGame.Scripts.Entities
{
    public class Spawner : MonoBehaviour, IEventReceiver<EntityCollisionEvent>
    {
        [SerializeField] private SpawnerConfig spawnerConfig;
        
        public EventBusReceiverIdentifier EventBusReceiverIdentifier { get; } = new();
        
        private IEventBus _eventBus;
        private IEntityFactory _entityFactory;
        private EntitiesConfig _entitiesConfig;
        private Timer _pauseTimer;
        private Entity _currentEntity;
        private int _maxSize;

        [Inject]
        public void Construct(IEventBus eventBus, IEntityFactory entityFactory, EntitiesConfig entitiesConfig)
        {
            _eventBus = eventBus;
            _entityFactory = entityFactory;
            _entitiesConfig = entitiesConfig;

            _entityFactory.OnCreate += (entity) =>
            {
                if (entity.SizeIndex > _maxSize)
                    _maxSize = entity.SizeIndex;
            };
            _eventBus.Subscribe(this);
        }

        private void Awake()
        {
            _pauseTimer = new Timer(spawnerConfig.PauseTime, spawnerConfig.PauseTime);
            _pauseTimer.OnTimerEnd += () =>
            {
                var fullWeight = spawnerConfig.GetFullWeight(_maxSize);
                var weight = Random.Range(0, fullWeight + 1);
                Debug.Log($"{_maxSize} | {fullWeight} | {weight}");
                var index = spawnerConfig.GetSize(_maxSize, weight);
                _currentEntity = _entityFactory.Create(index, transform.position);
                _currentEntity.DeActivate();
            };
        }

        private void Start()
        {
            _currentEntity = _entityFactory.Create(0, transform.position);
            _currentEntity.DeActivate();
        }

        private void Update()
        {
            _pauseTimer.Tick(Time.deltaTime);
        }

        public void Spawn()
        {
            if(!_pauseTimer.TimerIsEnd)
                return;

            _pauseTimer.Reset();
            _currentEntity.Activate();
            _currentEntity = null;
        }

        public void Move(Vector2 pos)
        {
            if(_currentEntity == null)
                return;

            pos.y = transform.position.y;
            pos.x = Mathf.Clamp(pos.x, transform.position.x - spawnerConfig.Range, transform.position.x + spawnerConfig.Range);
            _currentEntity.transform.position = pos;
        }

        public void OnEvent(EntityCollisionEvent t)
        {
            if(t.Parent.SizeIndex >= _entitiesConfig.Prefabs.Count - 1)
                return;

            var index = 1 + t.Parent.SizeIndex;
            var childPos = t.Child.transform.position;
            
            t.Parent.Return();
            t.Child.Return();

            _entityFactory.Create(index, childPos);
        }

        private void OnDestroy()
        {
            _eventBus?.UnSubscribe(this);
        }

        private void OnDrawGizmos()
        {
            if(spawnerConfig != null)
                Gizmos.DrawWireCube(transform.position, Vector3.up + Vector3.right * spawnerConfig.Range * 2);
        }
    }
}