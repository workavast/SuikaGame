using UnityEngine;

namespace SuikaGame.Scripts.GameplaySavers.AutoSaver
{
    [CreateAssetMenu(fileName = nameof(AutoGameplaySaverConfig), menuName = "SuikaGame/Configs/Saves/" + nameof(AutoGameplaySaverConfig))]
    public class AutoGameplaySaverConfig : ScriptableObject
    {
        [field: SerializeField, Min(0)] public float SavePause { get; private set; }
    }
}