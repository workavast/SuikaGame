using System.Collections.Generic;
using UnityEngine;

namespace SuikaGame.Scripts.Skins
{
    [CreateAssetMenu(fileName = nameof(SkinsPackConfig), menuName = "SuikaGame/Configs/Skins/" + nameof(SkinsPackConfig))]
    public class SkinsPackConfig : ScriptableObject
    {
        [SerializeField] private List<Sprite> sprites;

        public IReadOnlyList<Sprite> Sprites => sprites;
    }
}