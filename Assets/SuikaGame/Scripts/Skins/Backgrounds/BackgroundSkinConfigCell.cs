using System;
using UnityEngine;
using UnityEngine.AddressableAssets;

namespace SuikaGame.Scripts.Skins.Backgrounds
{
    [Serializable]
    public class BackgroundSkinConfigCell
    {
        [field: SerializeField, Min(0)] public int Price { get; private set; }
        [field: SerializeField] public Sprite Preview { get; private set; }
        [field: SerializeField] public AssetReferenceT<Texture2D> Background { get; private set; }
    }
}