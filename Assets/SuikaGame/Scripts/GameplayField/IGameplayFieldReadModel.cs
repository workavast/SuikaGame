using System;
using System.Collections.Generic;
using SuikaGame.Scripts.Entities;

namespace SuikaGame.Scripts.GameplayField
{
    public interface IGameplayFieldReadModel
    {
        public int Score { get; }
        public IEnumerable<EntityModel> EntityModels { get; }

        public event Action OnChange;
    }
}