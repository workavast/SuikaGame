using System;
using SuikaGame.Scripts.Entities;
using SuikaGame.Scripts.Entities.Factory;

namespace SuikaGame.Scripts.EntityMaxSizeCounting
{
    public class EntityMaxSizeCounter : IEntityMaxSizeCounter, IDisposable
    {
        public int CurrentMaxSize { get; private set; }
        public event Action<int> OnCurrentMaxSizeChanged;
        
        private readonly IEntityFactory _entityFactory;
        
        public EntityMaxSizeCounter(IEntityFactory entityFactory)
        {
            _entityFactory = entityFactory;
            _entityFactory.OnCreate += UpdateCurrentMaxSize;
        }

        private void UpdateCurrentMaxSize(Entity newEntity)
        {
            if (newEntity.SizeIndex > CurrentMaxSize)
            {
                CurrentMaxSize = newEntity.SizeIndex;
                OnCurrentMaxSizeChanged?.Invoke(CurrentMaxSize);
            }
        }

        public void Dispose()
        {
            if(_entityFactory != null)
                _entityFactory.OnCreate -= UpdateCurrentMaxSize;
        }
    }
}