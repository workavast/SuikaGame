namespace SuikaGame.Scripts.Analytics
{
    public interface IAnalyticsProvider
    {
        public void SendEvent(string key, int value, bool save = false);

        public void SendEvent(string key, string value = "", bool save = false);
    }
}