using System;

namespace SuikaGame.Scripts.Ad.AdTimer
{
    public interface IAdTimer
    {
        public bool IsPrepared { get; }
        
        public event Action OnAdPrepared;
    }
}