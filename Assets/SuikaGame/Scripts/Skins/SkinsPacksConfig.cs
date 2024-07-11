using System.Collections.Generic;
using Avastrad.DictionaryInspector;
using UnityEngine;

namespace SuikaGame.Scripts.Skins
{
    [CreateAssetMenu(fileName = nameof(SkinsPacksConfig), menuName = "SuikaGame/Configs/Skins/" + nameof(SkinsPacksConfig))]
    public class SkinsPacksConfig : ScriptableObject
    {
        [SerializeField] private SerializableDictionary<SkinPackType, SkinPackCell> skinsPacks;

        public IReadOnlyDictionary<SkinPackType, SkinPackCell> SkinsPacks => skinsPacks; 
    }
}