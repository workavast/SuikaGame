using System;
using System.Collections.Generic;
using System.Linq;
using SuikaGame.Scripts.Entities;

namespace SuikaGame.Scripts.Saves.GameplayScene
{
    public class GameplaySceneSettings : ISettings
    {
        public int Score;
        private List<EntityModel> _entityModels;
        public IEnumerable<EntityModel> EntityModels => _entityModels;
        
        public event Action OnChange;

        public GameplaySceneSettings()
        {
            Score = 0;
            _entityModels = new List<EntityModel>();
        }

        public void SetNewScore(int newSCore) 
            => Score = newSCore;
        
        public void SetNewEntityModels(IEnumerable<EntityModel> entityModels) 
            => _entityModels = entityModels.ToList();

        public void Apply() 
            => OnChange?.Invoke();
        
        public void LoadData(GameplaySceneSettingsSave settingsSave)
        {
            if (settingsSave?.EntityModels == null)
                _entityModels = new List<EntityModel>();
            else
                _entityModels = settingsSave.EntityModels.ToList();
        }
    }
}