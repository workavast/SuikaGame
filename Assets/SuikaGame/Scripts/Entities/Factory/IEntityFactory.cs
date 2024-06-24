using System;
using UnityEngine;

namespace SuikaGame.Scripts.Entities.Factory
{
    public interface IEntityFactory
    {
        public event Action<Entity> OnCreate; 
        
        public Entity Create(int index, Vector2 position);
    }
}