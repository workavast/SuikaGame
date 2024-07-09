using System;
using System.Collections.Generic;

namespace SuikaGame.Scripts.Entities
{
    public interface IEntitiesRepository
    {
        public event Action<Entity> OnAdd;
        
        public IReadOnlyList<Entity> Entities { get; }
        
        public void Reset();
    }
}