using System;
using System.Collections.Generic;
using System.Linq;
using SuikaGame.Scripts.Entities;
using SuikaGame.Scripts.GameplayField;

namespace SuikaGame.Scripts.Saves.GameplayScene
{
    public class GameplaySceneSettings : ISettings, IGameplayFieldModel
    {
        public int Score { get; private set; }
        private List<EntityModel> _entityModels;
        public IEnumerable<EntityModel> EntityModels => _entityModels;

        private int _prevScore;
        private readonly List<EntityModel> _prevEntityModels = new(8);
        
        public event Action OnChange;

        public GameplaySceneSettings()
        {
            Score = 0;
            _entityModels = new List<EntityModel>(8);
        }

        public void SetNewScore(int newSCore) 
            => Score = newSCore;

        public void SetNewEntityModels(IEnumerable<EntityModel> entityModels) 
            => _entityModels = entityModels.ToList();

        public void Apply()
        {
            if(_prevScore == Score && _prevEntityModels.Count <= 0 && _entityModels.Count <= 0)
                return;

            _prevScore = Score;
            
            _prevEntityModels.Clear();
            _prevEntityModels.AddRange(_entityModels);
            
            OnChange?.Invoke();
        }

        public void LoadData(GameplaySceneSettingsSave save)
        {
            Score = save.Score;
            if (save?.EntityModels == null)
                _entityModels = new List<EntityModel>();
            else
                _entityModels = save.EntityModels.ToList();
        }
    }
}