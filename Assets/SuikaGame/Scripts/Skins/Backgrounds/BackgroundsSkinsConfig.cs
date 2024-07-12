using System.Collections.Generic;
using Avastrad.DictionaryInspector;
using UnityEngine;

namespace SuikaGame.Scripts.Skins.Backgrounds
{
    [CreateAssetMenu(fileName = nameof(BackgroundsSkinsConfig), menuName = "SuikaGame/Configs/Skins/" + nameof(BackgroundsSkinsConfig))]
    public class BackgroundsSkinsConfig : ScriptableObject
    {
        [SerializeField] private SerializableDictionary<BackgroundSkinType, BackgroundSkinConfigCell> backgroundsSkins;

        public IReadOnlyDictionary<BackgroundSkinType, BackgroundSkinConfigCell> BackgroundsSkins => backgroundsSkins;
    }
}