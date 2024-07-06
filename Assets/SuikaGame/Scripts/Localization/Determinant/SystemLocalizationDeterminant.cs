using UnityEngine;

namespace SuikaGame.Scripts.Localization.Determinant
{
    public class SystemLocalizationDeterminant : ILocalizationDeterminant
    {
        public int GetLocalization()
        {
            switch (Application.systemLanguage)
            {
                case SystemLanguage.Russian:
                    return 1;
                case SystemLanguage.Ukrainian:
                    return 1;
                case SystemLanguage.English:
                    return 0;
                case SystemLanguage.Turkish:
                    return 0;
                default:
                    return 0;
            }   
        }
    }
}