using System;

namespace SuikaGame.Scripts.Entities.MaxSizeCounting
{
    public interface IEntityMaxSizeCounter
    {
        public int CurrentMaxSize { get; }
        
        public event Action<int> OnCurrentMaxSizeChanged;
        
        public void Reset();
    }
}