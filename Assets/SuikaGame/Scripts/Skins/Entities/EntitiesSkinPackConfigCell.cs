using System;
using UnityEngine;
using UnityEngine.AddressableAssets;

namespace SuikaGame.Scripts.Skins.Entities
{
    [Serializable]
    public class EntitiesSkinPackConfigCell
    {
        [field: SerializeField, Min(0)] public int Price { get; private set; }
        [field: SerializeField] public Sprite Preview { get; private set; }
        [field: SerializeField] public AssetReferenceT<EntitiesSkinPackConfig> SkinPack { get; private set; }
    }
}