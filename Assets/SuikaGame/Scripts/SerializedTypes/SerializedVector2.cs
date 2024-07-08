using System;
using UnityEngine;

namespace SuikaGame.Scripts.SerializedTypes
{
    [Serializable]
    public struct SerializedVector2
    {
        public float x;
        public float y;

        public SerializedVector2(Vector2 vector2)
        {
            x = vector2.x;
            y = vector2.y;
        }
        
        public SerializedVector2(Vector3 vector3)
        {
            x = vector3.x;
            y = vector3.y;
        }
        
        public SerializedVector2(Transform transform)
        {
            x = transform.position.x;
            y = transform.position.y;
        }
    }
}