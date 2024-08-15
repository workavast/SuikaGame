using System;
using System.Collections.Generic;
using System.Linq;

namespace SuikaGame.Scripts.Saves.Analytics
{
    [Serializable]
    public sealed class AnalyticsSettingsSave
    {
        public List<string> UsedEvents = new();

        public AnalyticsSettingsSave()
        {
            UsedEvents = new List<string>();
        }
        
        public AnalyticsSettingsSave(AnalyticsSettings settings)
        {
            UsedEvents = settings.UsedEvents.ToList();
        }
    }
}