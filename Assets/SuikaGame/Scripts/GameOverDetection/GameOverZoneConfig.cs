using UnityEngine;

namespace SuikaGame.Scripts.GameOverDetection
{
    [CreateAssetMenu(fileName = nameof(GameOverZoneConfig), menuName = "SuikaGame/Configs/" + nameof(GameOverZoneConfig))]
    public class GameOverZoneConfig : ScriptableObject
    {
        [field: SerializeField, Min(0)] public int Time { get; private set; }
        [field: SerializeField, Min(0)] public float VelocityBuffer { get; private set; }
    }
}