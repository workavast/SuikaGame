using System;

namespace SuikaGame.Scripts.Saves.Train
{
    public class TutorialSettings : ISettings
    {
        public bool TutorialCompleted { get; private set; }
        
        public event Action OnChange;

        public TutorialSettings()
        {
            TutorialCompleted = false;
        }

        public void SetTutorialState(bool trained)
        {
            if (TutorialCompleted == trained)
                return;
            
            TutorialCompleted = trained;
            OnChange?.Invoke();
        }
        
        public void LoadData(TutorialSettingsSave save)
        {
            TutorialCompleted = save.TutorialCompleted;
        }
    }
}