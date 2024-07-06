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
                case Language.Russian:
                    return 1;
                case Language.English:
                    return 0;
                case Language.Turkish:
                    return 0;
                default:
                    return 0;
            }   
        }
    }
}