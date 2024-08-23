using System;

namespace SuikaGame.Scripts.Saves.Train
{
    public class TutorialSettings : ISettings
    {
        public bool TutorialCompleted { get; private set; }

        public bool IsChanged { get; private set; }

        public TutorialSettings()
        {
            TutorialCompleted = false;
        }

        public void SetTutorialState(bool trained)
        {
            if (TutorialCompleted == trained)
                return;

            IsChanged = true;
            TutorialCompleted = trained;
        }
        
        public void LoadData(TutorialSettingsSave save)
        {
            TutorialCompleted = save.TutorialCompleted;
        }
        
        public void ResetChangedMarker() 
            => IsChanged = false;
    }
}