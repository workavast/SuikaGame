using System.Collections.Generic;
using UnityEngine;

namespace SuikaGame.Scripts.Entities
{
    [CreateAssetMenu(fileName = nameof(EntitiesConfig), menuName = "SuikaGame/Configs/" + nameof(EntitiesConfig))]
    public sealed class EntitiesConfig : ScriptableObject
    {
        [SerializeField] private List<Entity> prefabs;

        public IReadOnlyList<Entity> Prefabs => prefabs;
    }
}