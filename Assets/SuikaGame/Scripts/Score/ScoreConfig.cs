using UnityEngine;

namespace SuikaGame.Scripts
{
    [CreateAssetMenu(fileName = nameof(ScoreConfig), menuName = "SuikaGame/Configs/" + nameof(ScoreConfig))]
    public sealed class ScoreConfig : ScriptableObject
    {
        [SerializeField] private AnimationCurve scoreCurve;

        public int GetScore(int sizeIndex)
        {
            return (int)scoreCurve.Evaluate(sizeIndex);
        }
    }
}