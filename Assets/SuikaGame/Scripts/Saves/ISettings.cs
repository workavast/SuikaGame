using System;

namespace SuikaGame.Scripts.Saves
{
    public interface ISettings
    {
        public event Action OnChange;
    }
}