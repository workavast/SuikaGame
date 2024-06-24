using System;
using System.Collections.Generic;
using UnityEngine;

namespace SuikaGame.Scripts.Entities.Spawning
{
    [Serializable]
    public class SpawnSet
    {
        [SerializeField] private List<Block> set;

        public IReadOnlyList<Block> Set => set;
    }

    [Serializable]
    public class Block
    {
        [field: SerializeField] public int Size { get; private set; }
        [field: SerializeField] public int Weight { get; private set; }
    }
}