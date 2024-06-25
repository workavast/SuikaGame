using System;
using System.Collections.Generic;
using SuikaGame.Scripts.Entities;
using UnityEngine;

namespace SuikaGame.Scripts.GameOverDetection
{
    public class GameOverZone : MonoBehaviour
    {
        public event Action OnGameOver;

        private float timer = 5;
        private readonly List<Pair> _pairs = new();
        
        private void Update()
        {
            for (int i = 0; i < _pairs.Count; i++)
                _pairs[i].Tick(Time.deltaTime);
            
            for (int i = 0; i < _pairs.Count; i++)
                if (_pairs[i].Time >= timer)
                {
                    Debug.Log($"GAME OVER");
                    OnGameOver?.Invoke();
                    return;
                }
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.TryGetComponent(out Entity entity))
            {
                foreach (var pair in _pairs)
                {
                    if (pair.Entity == entity)
                    {
                        Debug.LogWarning($"Duplicate");
                        return;
                    }
                }

                _pairs.Add(new Pair(entity));
            }
        }

        private void OnTriggerExit2D(Collider2D other)
        {
            if (other.TryGetComponent(out Entity entity))
            {
                for (int i = 0; i < _pairs.Count; i++)
                {
                    if (_pairs[i].Entity == entity)
                    {
                        _pairs.RemoveAt(i);
                        return;
                    }
                }
            }
        }
        
        private class Pair
        {
            public readonly Entity Entity;
            public float Time { get; private set; }

            public Pair(Entity entity)
            {
                Entity = entity;
                Time = 0;
            }
            
            public void Tick(float deltaTime) 
                => Time += deltaTime;
        }
    }
}