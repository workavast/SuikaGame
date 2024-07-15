using System.IO;
using UnityEditor;
using UnityEditor.Build;
using UnityEditor.Build.Reporting;
using UnityEngine;

namespace SuikaGame.Scripts.DevTools.BuildVersion.Editor
{
    public class PreprocessBuildVersionChanger : IPreprocessBuildWithReport
    {
        public int callbackOrder { get; } = 1;
        
        public void OnPreprocessBuild(BuildReport report)
        {
            var json = Resources.Load<TextAsset>("BuildVersionConfiguration");
            var buildVersionHolder = JsonUtility.FromJson<BuildVersionHolder>(json.text);
            buildVersionHolder.BuildVersion++;
            var text = JsonUtility.ToJson(buildVersionHolder);
            
            File.WriteAllText(AssetDatabase.GetAssetPath(json), text);
            EditorUtility.SetDirty(json);
        }
    }
}