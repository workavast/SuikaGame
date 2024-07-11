using System.Collections.Generic;
using UnityEngine;

namespace SuikaGame.Scripts.Skins
{
    [CreateAssetMenu(fileName = nameof(SkinsPackConfig), menuName = "SuikaGame/Configs/Skins/" + nameof(SkinsPackConfig))]
    public class SkinsPackConfig : ScriptableObject
    {
        [field: SerializeField] public Sprite background;
        [SerializeField] private List<Sprite> sprites;

        public Sprite Background => background;
        public IReadOnlyList<Sprite> Sprites => sprites;
    }
}