using System.Collections.Generic;
using System.Linq;
using SuikaGame.Scripts.Entities;
using SuikaGame.Scripts.GameplayField.Model;

namespace SuikaGame.Scripts.Saves.GameplayScene
{
    public class GameplaySceneSettings : ISettings, IGameplayFieldModel
    {
        public bool IsChanged { get; private set; }
        
        public int Score { get; private set; }
        private List<EntityModel> _entityModels;
        public IEnumerable<EntityModel> EntityModels => _entityModels;
        
        public GameplaySceneSettings()
        {
            Score = 0;
            _entityModels = new List<EntityModel>(8);
        }

        public void SetNewScore(int newSCore)
        {
            if (Score == newSCore)
                return;

            IsChanged = true;
            Score = newSCore;
        }

        public void SetNewEntityModels(IEnumerable<EntityModel> entityModels)
        {
            if (entityModels.Count() <= 0 && _entityModels.Count <= 0)
                return;

            IsChanged = true;
            _entityModels = entityModels.ToList();
        }
        
        public void LoadData(GameplaySceneSettingsSave save)
        {
            if (save?.EntityModels == null)
                _entityModels = new List<EntityModel>();
            else
                _entityModels = save.EntityModels.ToList();
        }
        
        public void ResetChangedMarker() 
            => IsChanged = false;
    }
}