using Avastrad.CustomTimer;

namespace SuikaGame.Scripts.GameplayField.Savers.ManualSaver
{
    public interface IManualGameplaySaver
    {
        public bool SaveAllowed { get; }
        public IReadOnlyTimer ReadOnlyTimer { get; }

        public void Save();
    }
}