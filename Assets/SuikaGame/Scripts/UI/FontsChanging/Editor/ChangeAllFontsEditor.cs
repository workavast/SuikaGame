using TMPro;
using UnityEditor;
using UnityEngine;

namespace SuikaGame.Scripts.UI.FontsChanging.Editor
{
    public class ChangeAllFontsEditor : EditorWindow
    {
        private TMP_FontAsset newFont;

        [MenuItem("Tools/Swap All Fonts On The Scene")]
        public static void ShowWindow()
        {
            GetWindow<ChangeAllFontsEditor>("Swap All Fonts On The Scene");
        }

        private void OnGUI()
        {
            GUILayout.Label("Change All TextMeshPro Fonts", EditorStyles.boldLabel);

            newFont = (TMP_FontAsset)EditorGUILayout.ObjectField("New Font Asset", newFont, typeof(TMP_FontAsset), false);

            if (GUILayout.Button("Change Fonts"))
            {
                ChangeAllFonts();
            }
        }

        private void ChangeAllFonts()
        {
            if (newFont == null)
            {
                EditorUtility.DisplayDialog("Error", "Please assign a new font asset.", "OK");
                return;
            }

            // Find all TextMeshProUGUI objects in the scene
            TextMeshProUGUI[] textMeshProUGUIObjects = FindObjectsOfType<TextMeshProUGUI>(true);
            foreach (TextMeshProUGUI tmpUGUI in textMeshProUGUIObjects)
            {
                Undo.RecordObject(tmpUGUI, "Change Font");
                tmpUGUI.font = newFont;
                EditorUtility.SetDirty(tmpUGUI);
            }

            // Find all TextMeshPro objects in the scene
            TextMeshPro[] textMeshProObjects = FindObjectsOfType<TextMeshPro>(true);
            foreach (TextMeshPro tmp in textMeshProObjects)
            {
                Undo.RecordObject(tmp, "Change Font");
                tmp.font = newFont;
                EditorUtility.SetDirty(tmp);
            }

            EditorUtility.DisplayDialog("Success", "All fonts changed successfully.", "OK");
        }
    }

}