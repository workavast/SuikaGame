using UnityEngine;

namespace SuikaGame.Scripts.Coins
{
    [CreateAssetMenu(fileName = nameof(CoinsConfig), menuName = "SuikaGame/Configs/" + nameof(CoinsConfig))]
    public class CoinsConfig : ScriptableObject
    {
        [field: SerializeField, Range(0, 1), Tooltip("Percentage of score that will be converted into coins")]
        public float CoinsPerScore { get; private set; }
    }
}