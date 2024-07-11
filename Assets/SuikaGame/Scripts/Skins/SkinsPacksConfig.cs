using System.Collections.Generic;
using Avastrad.DictionaryInspector;
using UnityEngine;

namespace SuikaGame.Scripts.Skins
{
    [CreateAssetMenu(fileName = nameof(SkinsPacksConfig), menuName = "SuikaGame/Configs/Skins/" + nameof(SkinsPacksConfig))]
    public class SkinsPacksConfig : ScriptableObject
    {
        [SerializeField] private SerializableDictionary<SkinPackType, SkinPackConfigCell> skinsPacks;

        public IReadOnlyDictionary<SkinPackType, SkinPackConfigCell> SkinsPacks => skinsPacks; 
    }
}