using System;
using Avastrad.PoolSystem;
using UnityEngine;
using Zenject;

namespace SuikaGame.Scripts.Entities.Factory
{
    public class EntityFactory : MonoBehaviour, IEntityFactory
    {
        private DiContainer _diContainer;
        private EntitiesConfig _entitiesConfig;
        private Pool<Entity, int> _pool;
        
        public event Action<Entity> OnCreate;

        [Inject]
        public void Construct(DiContainer diContainer, EntitiesConfig entitiesConfig)
        {
            _diContainer = diContainer;
            _entitiesConfig = entitiesConfig;
        }
        
        private void Awake()
        {
            _pool = new Pool<Entity, int>(InstantiateEntity);
        }
        
        /// <param name="rotation"> z axis Quaternion rotation </param>
        public Entity Create(int index, Vector2 position, float rotation = 0)
        {
            _pool.ExtractElement(index, out var entity);
            
            entity.transform.SetPositionAndRotation(position, new Quaternion(0, 0, rotation, 1));

            OnCreate?.Invoke(entity);
            
            return entity;
        }

        private Entity InstantiateEntity(int index)
        {
            if (index >= _entitiesConfig.Prefabs.Count)
                throw new ArgumentOutOfRangeException(
                    $"index >= entitiesConfig.Prefabs.Count | {index} >= {_entitiesConfig.Prefabs.Count}");

            var entity = _diContainer.InstantiatePrefab(_entitiesConfig.Prefabs[index], transform)
                .GetComponent<Entity>();
            entity.Initialize(index);
            return entity;
        }
    }
}