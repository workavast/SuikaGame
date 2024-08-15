using System.Collections.Generic;
using SuikaGame.Scripts.Skins.Backgrounds;
using SuikaGame.Scripts.Skins.Entities;

namespace SuikaGame.Scripts.Analytics
{
    public static class AnalyticsKeys
    {
        public static readonly IReadOnlyList<string> Balls = new[]
        {
            "ball_0",
            "ball_1",
            "ball_2",
            "ball_3",
            "ball_4",
            "ball_5",
            "ball_6",
            "ball_7",
        };

        public static readonly IReadOnlyDictionary<EntitiesSkinPackType, string> SkinPacks =
            new Dictionary<EntitiesSkinPackType, string>()
            {
                { EntitiesSkinPackType.Fruits, "Fruits_SP" },
                { EntitiesSkinPackType.Cats, "Cats_SP" },
                { EntitiesSkinPackType.Dogs, "Dogs_SP" },
                { EntitiesSkinPackType.FruitsVector, "FruitsVector_SP" },
                { EntitiesSkinPackType.ExoticFruitsVector, "ExoticFruitsVector_SP" },
                { EntitiesSkinPackType.VegetablesVector, "VegetablesVector_SP" },
                { EntitiesSkinPackType.IceCream, "IceCream_SP" },
                { EntitiesSkinPackType.RuCoins, "RuCoins_SP" },
                { EntitiesSkinPackType.GravityFalls, "GravityFalls_SP" },
                { EntitiesSkinPackType.Pokemons, "Pokemons_SP" },
                { EntitiesSkinPackType.HayaoMiyazaki, "HayaoMiyazaki_SP" },
                { EntitiesSkinPackType.AdventureTime, "AdventureTime_SP" }
            };

        public static readonly IReadOnlyDictionary<BackgroundSkinType, string> BackgroundSkins =
            new Dictionary<BackgroundSkinType, string>()
            {
                { BackgroundSkinType.Fruits, "Fruits_BS" },
                { BackgroundSkinType.Cats, "Cats_BS" },
                { BackgroundSkinType.Dogs, "Dogs_BS" },
                { BackgroundSkinType.FruitsVector, "FruitsVector_BS" },
                { BackgroundSkinType.ExoticFruitsVector, "ExoticFruitsVector_BS" },
                { BackgroundSkinType.VegetablesVector, "VegetablesVector_BS" },
                { BackgroundSkinType.IceCream, "IceCream_BS" },
                { BackgroundSkinType.RuCoins, "RuCoins_BS" },
                { BackgroundSkinType.GravityFalls, "GravityFalls_BS" },
                { BackgroundSkinType.Pokemons, "Pokemons_BS" },
                { BackgroundSkinType.HayaoMiyazaki, "HayaoMiyazaki_BS" },
                { BackgroundSkinType.AdventureTime, "AdventureTime_BS" }
            };
    }
}