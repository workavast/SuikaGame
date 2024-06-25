using System.Collections.Generic;

namespace SuikaGame.Scripts.Entities
{
    public interface IEntitiesRepository
    {
        public IReadOnlyList<Entity> Entities { get; }
        
        public void Reset();
    }
}