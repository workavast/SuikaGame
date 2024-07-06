using System;
using GamePush;
using UnityEngine;
using Zenject;

namespace SuikaGame.Scripts.Ad.AdTimer
{
    public class GamePushAdTimer : IAdTimer, ITickable
    {
        private const int InitialPreparingTimer = 60;
        private float _initialTimer = 0;
        
        public bool IsPrepared { get; private set; }
        
        public event Action OnAdPrepared;
        
        public void Tick()
        {
            if (_initialTimer >= InitialPreparingTimer)
            {
                if (!IsPrepared && GP_Ads.IsFullscreenAvailable())
                {
                    IsPrepared = true;
                    OnAdPrepared?.Invoke();
                }
                else
                {
                    IsPrepared = false;
                }  
            }
            else
            {
                _initialTimer += Time.deltaTime;
            }
        }
    }
}