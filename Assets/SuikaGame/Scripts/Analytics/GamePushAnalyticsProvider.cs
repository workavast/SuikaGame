using System.Linq;
using GamePush;
using SuikaGame.Scripts.Saves;
using SuikaGame.Scripts.Saves.Analytics;

namespace SuikaGame.Scripts.Analytics
{
    public class GamePushAnalyticsProvider : IAnalyticsProvider
    {
        private readonly AnalyticsSettings _analyticsSettings;

        public GamePushAnalyticsProvider(AnalyticsSettings analyticsSettings)
        {
            _analyticsSettings = analyticsSettings;
        }
        
        public void SendEvent(string key, int value, bool save)
        {
            if (_analyticsSettings.UsedEvents.Contains(key))
                return;

            GP_Analytics.Goal(key, value);
            _analyticsSettings.AddUsedEvent(key);
            if (save)
                PlayerData.Instance.SaveData();
        }

        public void SendEvent(string key, string value, bool save)
        {
            if (_analyticsSettings.UsedEvents.Contains(key))
                return;
            
            GP_Analytics.Goal(key, value);
            _analyticsSettings.AddUsedEvent(key);
            if (save)
                PlayerData.Instance.SaveData();
        }
    }
}