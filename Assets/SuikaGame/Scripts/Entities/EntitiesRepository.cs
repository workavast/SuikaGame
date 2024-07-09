using System;
using System.Collections.Generic;
using System.Linq;
using SuikaGame.Scripts.Entities.Factory;
using UnityEngine;

namespace SuikaGame.Scripts.Entities
{
    public class EntitiesRepository : IEntitiesRepository
    {
        private readonly IEntityFactory _entityFactory;
        private List<Entity> _entities = new(4);

        public IReadOnlyList<Entity> Entities => _entities;

        public event Action<Entity> OnAdd;

        public EntitiesRepository(IEntityFactory entityFactory)
        {
            _entityFactory = entityFactory;
            _entityFactory.OnCreate += Add;
        }

        public void Reset()
        {
            var entities = _entities.ToList();
            foreach (var entity in entities) 
                entity.InstantlyReturnInPool();
        }
        
        private void Add(Entity newEntity)
        {
            if (_entities.Contains(newEntity))
            {
                Debug.LogWarning($"Duplicate of entity");
                return;
            }

            newEntity.ReturnElementEvent += Remove;
            newEntity.DestroyElementEvent += Remove;
            _entities.Add(newEntity);
            OnAdd?.Invoke(newEntity);
        }

        private void Remove(Entity entity)
        {
            entity.ReturnElementEvent -= Remove;
            entity.DestroyElementEvent -= Remove;
            _entities.Remove(entity);
        }
    }
}