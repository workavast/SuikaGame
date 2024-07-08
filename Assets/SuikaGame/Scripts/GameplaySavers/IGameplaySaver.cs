using System;

namespace SuikaGame.Scripts.GameplaySavers
{
    public interface IGameplaySaver
    {
        public event Action OnSave;
        
        public void Save();
    }
}