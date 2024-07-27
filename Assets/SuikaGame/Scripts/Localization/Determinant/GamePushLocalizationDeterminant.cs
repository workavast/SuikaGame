using GamePush;

namespace SuikaGame.Scripts.Localization.Determinant
{
    public class GamePushLocalizationDeterminant : ILocalizationDeterminant
    {
        public int GetLocalization()
        {
            var language = GP_Language.Current();
            switch (language)
            {
                case Language.English:
                    return 0;
                case Language.French:
                    return 1;
                case Language.German:
                    return 2;
                case Language.Italian:
                    return 3;
                case Language.Portuguese:
                    return 4;
                case Language.Russian:
                    return 5;
                case Language.Spanish:
                    return 6;
                case Language.Turkish:
                    return 7;
                default:
                    return 0;
            }   
        }
    }
}