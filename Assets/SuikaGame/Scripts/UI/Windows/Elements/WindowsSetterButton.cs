using Avastrad.UI.UI_System;
using UnityEngine;
using UnityEngine.UI;

namespace SuikaGame.Scripts.UI.Windows.Elements
{
    [DisallowMultipleComponent, RequireComponent(typeof(Button))]
    public class WindowsSetterButton : MonoBehaviour
    {
        [SerializeField] private ScreenType[] screenType;

        private void Awake() 
            => GetComponent<Button>().onClick.AddListener(SetWindows);

        protected virtual void SetWindows() 
            => UI_Controller.SetSingleScreens(screenType);

        private void OnDestroy() 
            => GetComponent<Button>().onClick.RemoveListener(SetWindows);
    }
}