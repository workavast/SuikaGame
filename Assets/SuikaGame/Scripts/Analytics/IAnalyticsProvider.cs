namespace SuikaGame.Scripts.Analytics
{
    public interface IAnalyticsProvider
    {
        public void SendEvent(string key, int value);

        public void SendEvent(string key, string value = "");
    }
}