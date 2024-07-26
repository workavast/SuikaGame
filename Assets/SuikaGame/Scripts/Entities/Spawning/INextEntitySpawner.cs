using System;

namespace SuikaGame.Scripts.Entities.Spawning
{
    public interface INextEntitySpawner
    {
        public int NextEntitySizeIndex { get; }
        public event Action<int> OnNextEntitySizeIndexChange;
    }
}