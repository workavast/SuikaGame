using System.Collections.Generic;
using UnityEngine;

namespace Avastrad.UI.UI_System
{
    [DisallowMultipleComponent]
    public class UI_Controller : MonoBehaviour
    {
        private void Start()
        {
            foreach (var screen in UI_ScreenRepository.Screens) 
                screen.Initialize();
        }

        public static void SetSingleScreens(IEnumerable<ScreenType> screenTypes)
        {
            foreach (var screen in UI_ScreenRepository.Screens)
                TryToggleScreen(screen, false);

            foreach (var screen in screenTypes) 
                ToggleScreen(screen, true);
        }
        
        public static void SetSingleScreen(ScreenType screenType)
        {
            foreach (var screen in UI_ScreenRepository.Screens) 
                screen.Hide();

            ToggleScreen(screenType, true);
        }
        
        public static void ToggleScreen(ScreenType screenType, bool show)
        {
            var screen = UI_ScreenRepository.GetScreenByEnum(screenType);
            TryToggleScreen(screen, show);
        }

        private static void TryToggleScreen(UI_ScreenBase screen, bool show)
        {
            if (screen.isActiveAndEnabled == show) 
                return;
            
            if (show)
                screen.Show();
            else
                screen.Hide();
        }
    }
}