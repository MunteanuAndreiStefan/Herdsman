using System.Collections;
using System.Collections.Generic;
using GameCore.Animal.Interfaces;
using GameCore.Spawn;
using UnityEngine;
using Utils;

namespace GameCore.Animal
{
    public class AnimalSpawner : IAnimalSpawner
    {
        private readonly IAnimalFactory _animalFactory;
        private ISpawnStrategy _spawnStrategy;
        private Dictionary<Material, HashSet<IAnimal>> _spawnedAnimals = new Dictionary<Material, HashSet<IAnimal>>();
        
        public AnimalSpawner(IAnimalFactory animalFactory) => _animalFactory = animalFactory;

        public void SetSpawnStrategy(ISpawnStrategy strategy) => _spawnStrategy = strategy;
        public void SpawnAnimal(string type) => SpawnAtPosition(_spawnStrategy.GetSpawnPosition());
        public void SpawnRandomAnimal() => SpawnAtPosition(_spawnStrategy.GetSpawnPosition());
        
        public Dictionary<Material, HashSet<IAnimal>> GetActiveAnimals() => _spawnedAnimals;
        
        public void DespawnAnimal(IAnimal animal) => _animalFactory.ReturnAnimal(animal);
        
        public IEnumerator SpawnRandomAnimalRoutine()
        {
            while (true)
            {
                var waitTime = Random.Range(Constants.DEFAULT_MIN_SPAWN_TIME, Constants.DEFAULT_MAX_SPAWN_TIME);
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