using System.Collections;
using System.Collections.Generic;
using GameCore.Animal.Interfaces;
using GameCore.Spawn;
using UnityEngine;

namespace GameCore.Animal
{
    public class AnimalSpawner : IAnimalSpawner
    {
        private readonly IAnimalFactory _animalFactory;
        private ISpawnStrategy _spawnStrategy;
        private Dictionary<Material, HashSet<IAnimal>> _spawnedAnimals = new Dictionary<Material, HashSet<IAnimal>>();
        
        public AnimalSpawner(IAnimalFactory animalFactory) => _animalFactory = animalFactory;

        /// <summary>
        /// Sets the spawn strategy for the animal spawner.
        /// </summary>
        /// <param name="strategy">Strategy to use</param>
        public void SetSpawnStrategy(ISpawnStrategy strategy) => _spawnStrategy = strategy;

        /// <summary>
        /// Spawns an animal based on the type and the current spawn strategy.
        /// </summary>
        /// <param name="type">Type of the animal wanted.</param>
        public void SpawnAnimal(string type) => SpawnAtPosition(_spawnStrategy.GetSpawnPosition());

        /// <summary>
        /// Spawns a random animal based on the current spawn strategy.
        /// </summary>
        public void SpawnRandomAnimal() => SpawnAtPosition(_spawnStrategy.GetSpawnPosition());
        
        /// <summary>
        /// Get the active animals in the scene as a dictionary.
        /// This is not optimal, we could structure material to animals mapping in a better way.
        /// But the performances boost from URP + GPU instancing is worth it.
        /// </summary>
        public Dictionary<Material, HashSet<IAnimal>> GetActiveAnimals() => _spawnedAnimals;
        
        /// <summary>
        /// Returns an animal to the object pool disposing it.
        /// </summary>
        /// <param name="animal">Animal wanted to be disposed.</param>
        public void DespawnAnimal(IAnimal animal) => _animalFactory.ReturnAnimal(animal);
        
        /// <summary>
        /// Coroutine for spawning random animals
        /// </summary>
        public IEnumerator SpawnRandomAnimalRoutine()
        {
            while (true)
            {
                var waitTime = Random.Range(DiContainer.Instance.GameConfig.MinSpawnTime, DiContainer.Instance.GameConfig.MaxSpawnTime);
                yield return new WaitForSeconds(waitTime);

                SpawnRandomAnimal();
            }
        }
        
        private void AddInstanceMatrix(IAnimal animal)
        {
            var material = animal.GetMaterial();

            if (!_spawnedAnimals.ContainsKey(material)) 
                _spawnedAnimals.Add(material, new HashSet<IAnimal>{animal});
            else
            {
                if (_spawnedAnimals[material].Contains(animal))
                    return;
                
                _spawnedAnimals[material].Add(animal);
            }
        }
        
        private void SpawnAtPosition(Vector3 position)
        {
            var animal = _animalFactory.CreateRandomAnimal();
            animal.Spawn(position);
            animal.OnDestroyed += () => RemoveInstanceMatrix(animal);
            AddInstanceMatrix(animal);
        }
        
        private void RemoveInstanceMatrix(IAnimal animal)
        {
            var material = animal.GetMaterial();
            if (!_spawnedAnimals.ContainsKey(material)) return;
            _spawnedAnimals[material].Remove(animal);
        }
    }
}