using System;
using System.Collections.Generic;
using Avastrad.UI.UI_System;
using SuikaGame.Scripts.Entities;
using UnityEngine;

namespace SuikaGame.Scripts.GameOverDetection
{
    public class GameOverZone : MonoBehaviour, IGameOverZone
    {
        [SerializeField] private StartEntityVelocityConfig startEntityVelocityConfig;
        [SerializeField] private GameOverZoneConfig gameOverZoneConfig;

        private readonly List<Pair> _pairs = new(2);
        private readonly List<Entity> _movedEntities = new(2);
        private readonly List<int> _removeBuffer = new(4);

        public int CurTime { get; private set; } = -1;
        public bool GameIsOver { get; private set; }

        public event Action OnEnterEntities;
        public event Action OnTimerUpdate;
        public event Action OnOutEntities;
        public event Action OnGameIsOver;

        private void Update()
        {
            if (GameIsOver)
                return;

            CheckMovingEntities();
            CheckUnMovingEntities();
            UpdateTimers();
            CheckTimersOver();
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.TryGetComponent(out Entity entity))
            {
                if (entity.Velocity.magnitude >= startEntityVelocityConfig.StartVelocity)
                {
                    if (_movedEntities.Contains(entity))
                    {
                        Debug.LogWarning($"Duplicate");
                        return;
                    }

                    _movedEntities.Add(entity);
                }
                else
                {
                    foreach (var pair in _pairs)
                        if (pair.Entity == entity)
                        {
                            Debug.LogWarning($"Duplicate");
                            return;
                        }

                    OnEnterEntities?.Invoke();
                    _pairs.Add(new Pair(entity));
                }
            }
        }

        private void OnTriggerExit2D(Collider2D other)
        {
            if (other.TryGetComponent(out Entity entity))
            {
                _movedEntities.Remove(entity);
                for (int i = 0; i < _pairs.Count; i++)
                    if (_pairs[i].Entity == entity)
                    {
                        _pairs.RemoveAt(i);
                        if (_pairs.Count <= 0)
                            OnOutEntities?.Invoke();
                        return;
                    }
            }
        }
        
        public void Reset()
        {
            GameIsOver = false;
            CurTime = -1;
        }

        private void CheckMovingEntities()
        {
            _removeBuffer.Clear();
            for (int i = 0; i < _pairs.Count; i++)
                if (_pairs[i].Entity.Velocity.magnitude >= startEntityVelocityConfig.StartVelocity)
                    _removeBuffer.Add(i);

            for (int i = _removeBuffer.Count - 1; i >= 0; i--)
            {
                var pairIndex = _removeBuffer[i];
                _movedEntities.Add(_pairs[pairIndex].Entity);
                _pairs.RemoveAt(pairIndex);
            }

            if (_pairs.Count <= 0)
                OnOutEntities?.Invoke();
        }

        private void CheckUnMovingEntities()
        {
            _removeBuffer.Clear();
            for (int i = 0; i < _movedEntities.Count; i++)
                if (_movedEntities[i].Velocity.magnitude < startEntityVelocityConfig.StartVelocity)
                    _removeBuffer.Add(i);

            var prevPairsCount = _pairs.Count;
            for (int i = _removeBuffer.Count - 1; i >= 0; i--)
            {
                var entityIndex = _removeBuffer[i];
                _pairs.Add(new Pair(_movedEntities[entityIndex]));
                _movedEntities.RemoveAt(entityIndex);
            }

            if (prevPairsCount < _pairs.Count)
                OnEnterEntities?.Invoke();
        }

        private void UpdateTimers()
        {
            var biggestTime = float.MinValue;
            for (int i = 0; i < _pairs.Count; i++)
            {
                _pairs[i].Tick(Time.deltaTime);
                if (biggestTime < _pairs[i].Timer)
                    biggestTime = _pairs[i].Timer;
            }

            var time = (int)Mathf.Ceil(gameOverZoneConfig.Time - biggestTime);
            if (time != CurTime)
            {
                CurTime = time;
                OnTimerUpdate?.Invoke();
            }
        }

        private void CheckTimersOver()
        {
            for (int i = 0; i < _pairs.Count; i++)
                if (_pairs[i].Timer >= gameOverZoneConfig.Time)
                {
                    Debug.Log($"GAME OVER");
                    GameIsOver = true;
                    OnGameIsOver?.Invoke();
                    return;
                }
        }

        private class Pair
        {
            public readonly Entity Entity;
            public float Timer { get; private set; }

            public Pair(Entity entity)
            {
                Entity = entity;
                Timer = 0;
            }

            public void Tick(float deltaTime)
                => Timer += deltaTime;
        }
    }
}