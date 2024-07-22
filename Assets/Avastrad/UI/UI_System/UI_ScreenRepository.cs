using System;
using System.Collections.Generic;
using System.Linq;
using SuikaGame.Scripts.UI.Windows;
using SuikaGame.Scripts.UI.Windows.Skins;
using UnityEngine;

namespace Avastrad.UI.UI_System
{
    public class UI_ScreenRepository : MonoBehaviour
    {
        private readonly Dictionary<Type, UI_ScreenBase> _screens = new();
    
        public static UI_ScreenRepository Instance;
        public static IReadOnlyList<UI_ScreenBase> Screens => Instance._screens.Values.ToArray();

        private void Awake()
        {
            if (Instance != null)
            {
                Destroy(this);
                return;
            }

            Instance = this;

            var screens = FindObjectsOfType<UI_ScreenBase>(true);
            foreach (var screen in screens) 
                _screens.Add(screen.GetType(), screen);
        }

        public static TScreen GetScreen<TScreen>() 
            where TScreen : UI_ScreenBase
        {
            if (Instance == null) 
                throw new NullReferenceException($"Instance is null");
        
            if(Instance._screens.TryGetValue(typeof(TScreen), out var screen)) 
                return (TScreen)screen;

            return default;
        }
        
        public static UI_ScreenBase GetScreenByEnum(ScreenType screenType)
        {
            if (Instance == null) 
                throw new NullReferenceException($"Instance is null");
        
            switch (screenType)
            {
                case ScreenType.MainMenu:
                    return GetScreen<MainMenuWindow>();
                case ScreenType.MainMenuSettings:
                    return GetScreen<MainMenuSettingsWindow>();
                case ScreenType.Skins:
                    return GetScreen<SkinsWindow>();
                case ScreenType.Gameplay:
                    return GetScreen<GameplayWindow>();
                case ScreenType.GameplaySettings:
                    return GetScreen<GameplaySettingsWindow>();
                case ScreenType.GameplayEnd:
                    return GetScreen<GameplayEndWindow>();
                case ScreenType.RewardedAd:
                    return GetScreen<RewardAdWindow>();
                case ScreenType.BottomMenu:
                    return GetScreen<BottomMenu>();
                default:
                    throw new ArgumentOutOfRangeException($"invalid parameter: {screenType}");
            }
        }
    }
}