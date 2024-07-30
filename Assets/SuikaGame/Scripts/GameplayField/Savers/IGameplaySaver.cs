using System;

namespace SuikaGame.Scripts.GameplayField.Savers
{
    public interface IGameplaySaver
    {
        public event Action OnSave;
        
        public void Save();
    }
}