using System;
using UnityEngine;

namespace SuikaGame.Scripts.InputDetection
{
    public interface IInput
    {
        public event Action<Vector2> Pressed;
        public event Action<Vector2> Hold;
        public event Action<Vector2> Release;
    }
}