using System.Collections;
using System.Collections.Generic;
using GameCore.Spawn;
using UnityEngine;

namespace GameCore.Animal.Interfaces
{
    /// <summary>
    /// AnimalSpawner interface
    /// </summary>
    public interface IAnimalSpawner
    {
        public void SetSpawnStrategy(ISpawnStrategy strategy);
        public void SpawnAnimal(string type);
        public void SpawnRandomAnimal();
        public IEnumerator SpawnRandomAnimalRoutine();
        public void DespawnAnimal(IAnimal animal);
        public Dictionary<Material, HashSet<IAnimal>> GetActiveAnimals();
    }
}