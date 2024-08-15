﻿using System;
using System.Collections.Generic;

namespace SuikaGame.Scripts.Saves.Analytics
{
    public sealed class AnalyticsSettings : ISettings
    {
        private readonly List<string> _usedEvents = new();
        public IReadOnlyList<string> UsedEvents => _usedEvents;
        
        public event Action OnChange;

        public AnalyticsSettings()
        {
            _usedEvents = new List<string>();
        }

        public void AddUsedEvent(string usedEvent)
        {
            if (!_usedEvents.Contains(usedEvent)) 
                _usedEvents.Add(usedEvent);
        }

        public void Apply()
        {
            OnChange?.Invoke();
        }
        
        public void LoadData(AnalyticsSettingsSave save)
        {
            _usedEvents.Clear();
            _usedEvents.AddRange(save.UsedEvents);
        }
    }
}