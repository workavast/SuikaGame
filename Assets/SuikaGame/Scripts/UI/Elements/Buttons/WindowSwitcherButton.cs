using Avastrad.UI.UI_System;
using UnityEngine;
using UnityEngine.UI;

namespace SuikaGame.Scripts.UI.Elements.Buttons
{
    [DisallowMultipleComponent, RequireComponent(typeof(Button))]
    public class WindowSwitcherButton : MonoBehaviour
    {
        [SerializeField] private ScreenType screenType;

        public virtual void _OpenWindow() 
            => UI_Controller.ToggleScreen(screenType, true);
        
        public virtual void _CloseWindow() 
            => UI_Controller.ToggleScreen(screenType, false);
    }
}