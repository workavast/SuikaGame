using System;
using Avastrad.SavingAndLoading;
using UnityEngine;

namespace SuikaGame.Scripts.Saves
{
    public class DesktopSaveAndLoader : IPlayerDataSaveAndLoader
    {
        private readonly ISaveAndLoader _fileSaveAndLoader = new JsonSaveAndLoader();

        public event Action<PlayerDataSave> OnLoaded;

        public void Load()
        {
            var save = _fileSaveAndLoader.Load<PlayerDataSave>();
            OnLoaded?.Invoke(save);
        }

        public void Save(PlayerData playerData)
        {
            _fileSaveAndLoader.Save(new PlayerDataSave(playerData));
        }

        public void ResetSave() 
            => _fileSaveAndLoader.Save(new PlayerDataSave());
    }
}