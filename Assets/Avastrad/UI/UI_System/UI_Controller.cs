using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Avastrad.UI.UI_System
{
    public class UI_Controller : MonoBehaviour
    {
        private static List<GameObject> UI_Activies = new List<GameObject>();
        private static List<GameObject> UI_PrevActivies = new List<GameObject>();
        private static List<GameObject> buffer = new List<GameObject>();

        void Start()
        {
            var activeScreens = new List<GameObject>();
            foreach (var screen in UI_ScreenRepository.Screens)
                if (screen.isActiveAndEnabled) activeScreens.Add(screen.gameObject);

            UI_Activies = activeScreens;
            if (UI_Activies.Count <= 0) Debug.LogError("No have active screen");
        
            UI_PrevActivies = UI_Activies;
        }

        public static void SwitchScreen(ScreenEnum screen, bool setActive)
        {
            UI_ScreenBase addScreen = UI_ScreenRepository.GetScreenByEnum(screen);
            if (addScreen.isActiveAndEnabled != setActive)
                addScreen.gameObject.SetActive(setActive);
        }
        public static void SetScreens(List<ScreenEnum> screens)
        {
            var newActiveScreens = new List<GameObject>();
        
            foreach (var screen in UI_Activies) screen.gameObject.SetActive(false);
            foreach (var screen in screens)
            {
                UI_ScreenBase newScreen = UI_ScreenRepository.GetScreenByEnum(screen);
                if (!newScreen.isActiveAndEnabled) newScreen.gameObject.SetActive(true);
                newActiveScreens.Add(newScreen.gameObject);
            }

            UI_PrevActivies = UI_Activies;
            UI_Activies = newActiveScreens;
        }

        public static void LoadScene(int sceneBuildIndex)
        {
            if (sceneBuildIndex == -1)
            {
                int currentSceneNum = SceneManager.GetActiveScene().buildIndex;
                SceneManager.LoadScene(currentSceneNum, LoadSceneMode.Single);
            }
            else
            {
                SceneManager.LoadScene(sceneBuildIndex);
            }
        }

        public static void Quit()
        {
            Application.Quit();
        }
    }
}