using System.Collections.Generic;
using SuikaGame.Scripts.Entities;

namespace SuikaGame.Scripts.GameplayField.Model
{
    public interface IGameplayFieldReadModel
    {
        public int Score { get; }
        public IEnumerable<EntityModel> EntityModels { get; }
    }
}