namespace SuikaGame.Scripts.Saves.Score
{
    public class ScoreSettings : ISettings
    {
        public bool IsChanged { get; private set; }
        
        public int ScoreRecord { get; private set; }

        public ScoreSettings()
        {
            ScoreRecord = 0;
        }

        public void SetScoreRecord(int newScoreRecord)
        {
            if (newScoreRecord == ScoreRecord)
                return;
            
            IsChanged = true;
            ScoreRecord = newScoreRecord;
        }
        
        public void LoadData(ScoreSettingsSave save)
        {
            ScoreRecord = save.ScoreRecord;
        }
        
        public void ResetChangedMarker() 
            => IsChanged = false;
    }
}