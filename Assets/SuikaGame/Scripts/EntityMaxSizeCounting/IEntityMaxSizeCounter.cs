using System;

namespace SuikaGame.Scripts.EntityMaxSizeCounting
{
    public interface IEntityMaxSizeCounter
    {
        public int CurrentMaxSize { get; }
        
        public event Action<int> OnCurrentMaxSizeChanged;
        
        public void Reset();
    }
}