using TMPro;
using UnityEngine;

namespace SuikaGame.Scripts.DevTools.BuildVersion
{
    public class BuildVersionView : MonoBehaviour
    {
        [SerializeField] private TextAsset buildVersionConfiguration;
        [SerializeField] private TMP_Text tmpText;

        private void Awake()
        {
            DontDestroyOnLoad(gameObject);
            var buildVersionHolder = JsonUtility.FromJson<BuildVersionHolder>(buildVersionConfiguration.text);
            tmpText.text = "Build Version: " + buildVersionHolder.BuildVersion;
        }
    }
}