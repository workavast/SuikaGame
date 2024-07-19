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

        public static void SetSingleScreen(ScreenType screenType)
        {
            foreach (var screen in UI_ScreenRepository.Screens) 
                screen.Hide();

            ToggleScreen(screenType, true);
        }
        
        public static void ToggleScreen(ScreenType screenType, bool show)
        {
            var screen = UI_ScreenRepository.GetScreenByEnum(screenType);

            if (screen.isActiveAndEnabled != show)
            {
                if (show)
                    screen.Show();
                else
                    screen.Hide();
            }
        }
    }
}