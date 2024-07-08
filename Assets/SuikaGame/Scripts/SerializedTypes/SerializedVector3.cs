using System;
using UnityEngine;

namespace SuikaGame.Scripts.SerializedTypes
{
    [Serializable]
    public struct SerializedVector3
    {
        public float x;
        public float y;
        public float z;
        
        public SerializedVector3(Vector2 vector2)
        {
            x = vector2.x;
            y = vector2.y;
            z = 0;
        }
        
        public SerializedVector3(Vector3 vector3)
        {
            x = vector3.x;
            y = vector3.y;
            z = vector3.z;
        }
        
        public SerializedVector3(Transform transform)
        {
            x = transform.position.x;
            y = transform.position.y;
            z = transform.position.z;
        }
    }
}