using System.Collections.Generic;
using Avastrad.DictionaryInspector;
using UnityEngine;

namespace SuikaGame.Scripts.Skins.Entities
{
    [CreateAssetMenu(fileName = nameof(EntitiesSkinPacksConfig), menuName = "SuikaGame/Configs/Skins/" + nameof(EntitiesSkinPacksConfig))]
    public class EntitiesSkinPacksConfig : ScriptableObject
    {
        [SerializeField] private SerializableDictionary<EntitiesSkinPackType, EntitiesSkinPackConfigCell> skinsPacks;

        public IReadOnlyDictionary<EntitiesSkinPackType, EntitiesSkinPackConfigCell> SkinsPacks => skinsPacks; 
    }
}