using System.Collections;
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

        public AnimalSpawner(IAnimalFactory animalFactory) => _animalFactory = animalFactory;

        public void SetSpawnStrategy(ISpawnStrategy strategy) => _spawnStrategy = strategy;

        public void SpawnAnimal(string type)
        {
            var position = _spawnStrategy.GetSpawnPosition();
            var animal = _animalFactory.CreateAnimal(type);
            animal.Spawn(position);
        }

        public void SpawnRandomAnimal()
        {
            var position = _spawnStrategy.GetSpawnPosition();
            var animal = _animalFactory.CreateRandomAnimal();
            animal.Spawn(position);
        }

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
    }
}