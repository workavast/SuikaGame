using System;

namespace SuikaGame.Scripts.Saves.Score
{
    public class ScoreSettings : ISettings
    {
        public int ScoreRecord { get; private set; }
        
        public event Action OnChange;
        
        public ScoreSettings()
        {
            ScoreRecord = 0;
        }

        public void SetScoreRecord(int newScoreRecord)
        {
            if (newScoreRecord == ScoreRecord)
                return;
            
            ScoreRecord = newScoreRecord;
            OnChange?.Invoke();
        }
        
        public void LoadData(ScoreSettingsSave save)
        {
            ScoreRecord = save.ScoreRecord;
        }
    }
}