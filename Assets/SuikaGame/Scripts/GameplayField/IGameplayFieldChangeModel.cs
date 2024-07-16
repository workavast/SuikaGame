using System.Collections.Generic;
using SuikaGame.Scripts.Entities;

namespace SuikaGame.Scripts.GameplayField
{
    public interface IGameplayFieldChangeModel
    {
        public void SetNewScore(int newSCore);

        public void SetNewEntityModels(IEnumerable<EntityModel> entityModels);

        public void Apply();
    }
}