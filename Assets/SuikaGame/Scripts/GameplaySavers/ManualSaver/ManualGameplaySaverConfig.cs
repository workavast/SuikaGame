using UnityEngine;

namespace SuikaGame.Scripts.GameplaySavers.ManualSaver
{
    [CreateAssetMenu(fileName = nameof(ManualGameplaySaverConfig), menuName = "SuikaGame/Configs/Saves/" + nameof(ManualGameplaySaverConfig))]
    public class ManualGameplaySaverConfig : ScriptableObject
    {
        [field: SerializeField, Min(0)] public float SavePause { get; private set; }
    }
}