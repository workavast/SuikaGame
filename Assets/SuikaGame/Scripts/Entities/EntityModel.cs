using System;
using SuikaGame.Scripts.SerializedTypes;
using UnityEngine;

namespace SuikaGame.Scripts.Entities
{
    [Serializable]
    public class EntityModel
    {
        public SerializedVector2 position;
        public float rotation;
        public int sizeIndex;

        public Vector2 Vector2Position => new Vector2(position.x, position.y);
        
        public EntityModel(Entity entity)
        {
            position = new SerializedVector2(entity.transform.position);
            rotation = entity.transform.rotation.z;
            sizeIndex = entity.SizeIndex;
        }
    }
}