using System;

namespace SuikaGame.Scripts.Saves
{
    public interface IPlayerDataSaveAndLoader
    {
        public event Action<PlayerDataSave> OnLoaded;
        
        public void Load();
        public void Save(PlayerData playerData);
        public void ResetSave();
    }
}