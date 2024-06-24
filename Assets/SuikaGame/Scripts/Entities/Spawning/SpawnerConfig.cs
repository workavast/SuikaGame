using System.Collections.Generic;
using System.Linq;
using SuikaGame.Scripts.Entities.Spawning;
using UnityEngine;

namespace SuikaGame.Scripts
{
    [CreateAssetMenu(fileName = nameof(SpawnerConfig), menuName = "SuikaGame/Configs/" + nameof(SpawnerConfig))]
    public class SpawnerConfig : ScriptableObject
    {
        [field: SerializeField] public float PauseTime { get; private set; }
        [field: SerializeField] public float Range { get; private set; }

        [SerializeField] private List<SpawnSet> spawnSets;
     
        public IReadOnlyList<SpawnSet> SpawnSets => spawnSets;

        public int GetFullWeight(int currentMaxSize)
        {
            if (currentMaxSize >= spawnSets.Count)
                currentMaxSize = spawnSets.Count - 1;
            
            var fullWeight = 0;
            foreach (var block in spawnSets[currentMaxSize].Set) 
                fullWeight += block.Weight;

            return fullWeight;
        }

        public int GetSize(int currentMaxSize, int randomNumber)
        {
            if (currentMaxSize >= spawnSets.Count)
                currentMaxSize = spawnSets.Count - 1;
            
            if(randomNumber <= 0)
                return spawnSets[currentMaxSize].Set.First().Size;
            
            var minWeight = 0;
            foreach (var block in spawnSets[currentMaxSize].Set)
            {
                if (minWeight <= randomNumber && randomNumber <= block.Weight)
                    return block.Size;
                minWeight += block.Weight;
            }

            return spawnSets[currentMaxSize].Set.Last().Size;
        }
    }
}