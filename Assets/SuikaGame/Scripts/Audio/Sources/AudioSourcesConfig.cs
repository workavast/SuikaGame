using System;
using System.Collections.Generic;
using Avastrad.DictionaryInspector;
using UnityEngine;

namespace SuikaGame.Scripts.Audio.Sources
{
    [CreateAssetMenu(fileName = nameof(AudioSourcesConfig), menuName = "SuikaGame/Configs/" + nameof(AudioSourcesConfig))]
    public class AudioSourcesConfig : ScriptableObject
    {
        [SerializeField] private SerializableDictionary<AudioSourceType, AudioCell> data;

        public IReadOnlyDictionary<AudioSourceType, AudioCell> Data => data;
    }

    [Serializable]
    public class AudioCell
    {
        [field: SerializeField] public AudioSourceHolder AudioSourceHolderPrefab { get; private set; }
        [field: SerializeField] public float Pitch { get; private set; }
        [field: SerializeField] public float PitchRange { get; private set; }
    }
}