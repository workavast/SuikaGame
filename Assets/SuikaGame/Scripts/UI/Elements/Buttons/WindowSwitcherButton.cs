using Avastrad.UI.UI_System;
using UnityEngine;
using UnityEngine.UI;

namespace SuikaGame.Scripts.UI.Elements.Buttons
{
    [DisallowMultipleComponent, RequireComponent(typeof(Button))]
    public class WindowSwitcherButton : MonoBehaviour
    {
        [SerializeField] private ScreenType screenType;
        [SerializeField] private bool show;

        private void Awake() 
            => GetComponent<Button>().onClick.AddListener(ToggleWindow);

        private void ToggleWindow()
        {
            if (show)
                _OpenWindow();
            else
                _CloseWindow();
        }

        public virtual void _OpenWindow() 
            => UI_Controller.ToggleScreen(screenType, true);
        
        public virtual void _CloseWindow() 
            => UI_Controller.ToggleScreen(screenType, false);

        private void OnDestroy() 
            => GetComponent<Button>().onClick.RemoveListener(ToggleWindow);
    }
}