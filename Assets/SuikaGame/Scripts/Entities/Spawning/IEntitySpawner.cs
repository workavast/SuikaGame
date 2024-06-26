using System;
using UnityEngine;

namespace SuikaGame.Scripts.Entities.Spawning
{
    public interface IEntitySpawner
    {
        public event Action OnSpawnEntity;
        public event Action OnDeSpawnEntity;
        public event Action<Vector2> OnMoveEntity; 

        public void Reset();
    }
}