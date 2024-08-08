using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;

namespace SuikaGame.Scripts.Skins.Entities
{
    [Serializable]
    public class EntitiesSkinPackConfigCell
    {
        [field: SerializeField, Min(0)] public int Price { get; private set; }
        [field: SerializeField] public AssetReferenceT<EntitiesSkinPackConfig> SkinPack { get; private set; }
        [SerializeField] private List<Sprite> preview;
        
        public IReadOnlyList<Sprite> Preview => preview;
    }
}