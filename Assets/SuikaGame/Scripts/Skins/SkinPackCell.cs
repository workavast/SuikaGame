using System;
using UnityEngine;
using UnityEngine.AddressableAssets;

namespace SuikaGame.Scripts.Skins
{
    [Serializable]
    public class SkinPackCell
    {
        [field: SerializeField] public AssetReferenceT<SkinsPackConfig> SkinPack { get; private set; }
        [field: SerializeField] public Sprite BackgroundPreview { get; private set; }
        [field: SerializeField] public Sprite EntitiesPreview { get; private set; }
    }
}