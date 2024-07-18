using SuikaGame.Scripts.Localization.Changer;
using UnityEngine.UI;
using Zenject;

namespace SuikaGame.Scripts.UI.Elements.Buttons
{
    public class LocalizationButton : Button
    {
        private ILocalizationChanger _localizationManager;

        [Inject]
        public void Construct(ILocalizationChanger localizationManager)
        {
            _localizationManager = localizationManager;
        }
        
        public void _ChangeLocalization(int localizationIndex) 
            => _localizationManager.ChangeLocalization(localizationIndex);
    }
}
