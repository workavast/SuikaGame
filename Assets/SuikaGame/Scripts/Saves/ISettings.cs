namespace SuikaGame.Scripts.Saves
{
    public interface ISettings
    {
        public bool IsChanged { get; }

        public void ResetChangedMarker();
    }
}