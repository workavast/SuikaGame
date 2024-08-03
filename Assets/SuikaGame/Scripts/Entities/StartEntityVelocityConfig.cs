using UnityEngine;

namespace SuikaGame.Scripts.Entities
{
    [CreateAssetMenu(fileName = nameof(StartEntityVelocityConfig), menuName = "SuikaGame/Configs/" + nameof(StartEntityVelocityConfig))]
    public class StartEntityVelocityConfig : ScriptableObject
    {
        [field: SerializeField] public float StartVelocity { get; private set; }
    }
}