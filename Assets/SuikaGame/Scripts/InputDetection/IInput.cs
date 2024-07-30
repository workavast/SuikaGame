using System;
using UnityEngine;

namespace SuikaGame.Scripts.InputDetection
{
    public interface IInput
    {
        public bool IsHold { get; }
        public Vector2 HoldPoint { get; }
        
        public event Action<Vector2> Pressed;
        public event Action<Vector2> Hold;
        public event Action<Vector2> Release;
    }
}