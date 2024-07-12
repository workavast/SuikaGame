using System.Collections.Generic;
using UnityEngine;

namespace SuikaGame.Scripts.Skins.Entities
{
    [CreateAssetMenu(fileName = nameof(EntitiesSkinPackConfig), menuName = "SuikaGame/Configs/Skins/" + nameof(EntitiesSkinPackConfig))]
    public class EntitiesSkinPackConfig : ScriptableObject
    {
        [SerializeField] private List<Sprite> skins;

        public IReadOnlyList<Sprite> Skins => skins;
    }
}