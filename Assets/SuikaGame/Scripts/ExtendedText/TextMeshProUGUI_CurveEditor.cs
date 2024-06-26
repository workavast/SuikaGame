using TMPro;
using TMPro.EditorUtilities;
using UnityEditor;
using UnityEngine;

namespace SuikaGame.Scripts
{
    [CustomEditor(typeof(TextMeshProUGUI_Curve))]
    public class TextMeshProUGUI_CurveEditor : TMP_EditorPanelUI
    {
        private SerializedProperty _vertexCurve;
        private SerializedProperty _curveScale;
        private SerializedProperty _curveScale2;
        
        
        
        protected override void OnEnable()
        {
            base.OnEnable();
            
            _curveScale2 = serializedObject.FindProperty("m_text");
            _vertexCurve = serializedObject.FindProperty("vertexCurve");
            _curveScale = serializedObject.FindProperty("curveScale");
        }

        protected override void DrawExtraSettings()
        {
            DrawCurveData();
            base.DrawExtraSettings();
        }

        private void DrawCurveData()
        {
            EditorGUI.BeginChangeCheck();
            EditorGUILayout.PropertyField(_vertexCurve);
            if (EditorGUI.EndChangeCheck())
            {
                // m_HavePropertiesChanged = true;
            }
            
            
            EditorGUI.BeginChangeCheck();
            EditorGUILayout.PropertyField(_curveScale);
            if (EditorGUI.EndChangeCheck())
            {
                // m_HavePropertiesChanged = true;
            }
        }
        
        protected override bool IsMixSelectionTypes()
        {
            GameObject[] objects = Selection.gameObjects;
            if (objects.Length > 1)
            {
                for (int i = 0; i < objects.Length; i++)
                {
                    if (objects[i].GetComponent<TextMeshProUGUI_Curve>() == null)
                        return true;
                }
            }
            return false;
        }
        
        protected override void OnUndoRedo()
        {
            int undoEventId = Undo.GetCurrentGroup();
            int lastUndoEventId = s_EventId;

            if (undoEventId != lastUndoEventId)
            {
                for (int i = 0; i < targets.Length; i++)
                {
                    //Debug.Log("Undo & Redo Performed detected in Editor Panel. Event ID:" + Undo.GetCurrentGroup());
                    TMPro_EventManager.ON_TEXTMESHPRO_UGUI_PROPERTY_CHANGED(true, targets[i] as TextMeshProUGUI_Curve);
                    s_EventId = undoEventId;
                }
            }
        }
    }
}