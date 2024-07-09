using System.Collections.Generic;
using Avastrad.DictionaryInspector;
using UnityEngine;
using UnityEngine.AddressableAssets;

namespace SuikaGame.Scripts.Skins
{
    [CreateAssetMenu(fileName = nameof(SkinsPacksConfig), menuName = "SuikaGame/Configs/Skins/" + nameof(SkinsPacksConfig))]
    public class SkinsPacksConfig : ScriptableObject
    {
        [SerializeField] private SerializableDictionary<SkinPackType, AssetReferenceT<SkinsPackConfig>> skinsPacks;

        public IReadOnlyDictionary<SkinPackType, AssetReferenceT<SkinsPackConfig>> SkinsPacks => skinsPacks;
    }
}