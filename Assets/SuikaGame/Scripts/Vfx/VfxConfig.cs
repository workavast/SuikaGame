using System.Collections.Generic;
using Avastrad.DictionaryInspector;
using UnityEngine;

namespace SuikaGame.Scripts.Vfx
{
    [CreateAssetMenu(fileName = nameof(VfxConfig), menuName = "SuikaGame/Configs/" + nameof(VfxConfig))]
    public class VfxConfig : ScriptableObject
    {
        [SerializeField] private SerializableDictionary<VfxType, VfxHolder> prefabs;

        public IReadOnlyDictionary<VfxType, VfxHolder> Prefabs => prefabs;
    }
}