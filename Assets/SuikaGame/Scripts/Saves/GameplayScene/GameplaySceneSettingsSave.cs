using System;
using System.Collections.Generic;
using System.Linq;
using SuikaGame.Scripts.Entities;

namespace SuikaGame.Scripts.Saves.GameplayScene
{
    [Serializable]
    public class GameplaySceneSettingsSave
    {
        public int Score;
        public List<EntityModel> EntityModels;

        public GameplaySceneSettingsSave()
        {
            Score = 0;
            EntityModels = new List<EntityModel>();
        }
        
        public GameplaySceneSettingsSave(GameplaySceneSettings settings)
        {
            Score = settings.Score;
            EntityModels = settings.EntityModels.ToList();
        }
    }
}