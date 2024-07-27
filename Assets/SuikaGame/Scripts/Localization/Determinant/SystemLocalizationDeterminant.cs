using UnityEngine;

namespace SuikaGame.Scripts.Localization.Determinant
{
    public class SystemLocalizationDeterminant : ILocalizationDeterminant
    {
        public int GetLocalization()
        {
            switch (Application.systemLanguage)
            {
                case SystemLanguage.English:
                    return 0;
                case SystemLanguage.French:
                    return 1;
                case SystemLanguage.German:
                    return 2;
                case SystemLanguage.Italian:
                    return 3;
                case SystemLanguage.Portuguese:
                    return 4;
                case SystemLanguage.Russian:
                    return 5;
                case SystemLanguage.Ukrainian:
                    return 5;
                case SystemLanguage.Spanish:
                    return 6;
                case SystemLanguage.Turkish:
                    return 7;
                default:
                    return 0;
            }   
        }
    }
}