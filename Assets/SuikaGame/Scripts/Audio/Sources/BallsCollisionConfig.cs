using SuikaGame.Scripts.Audio.Sources;
using UnityEngine;

namespace SuikaGame.Scripts.Audio
{
    [CreateAssetMenu(fileName = nameof(BallsCollisionConfig), menuName = "SuikaGame/Configs/" + nameof(BallsCollisionConfig))]
    public class BallsCollisionConfig : ScriptableObject
    {
        [field: SerializeField] public float minimalSpeedForAudio;
        [field: SerializeField] public AudioSourceType collisionAudioType;
    }
}