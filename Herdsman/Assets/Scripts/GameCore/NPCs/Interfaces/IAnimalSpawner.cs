using System.Collections;
using GameCore.Spawn;

namespace GameCore.Animal.Interfaces
{
    public interface IAnimalSpawner
    {
        void SetSpawnStrategy(ISpawnStrategy strategy);
        void SpawnAnimal(string type);
        void SpawnRandomAnimal();
        IEnumerator SpawnRandomAnimalRoutine();
        void DespawnAnimal(IAnimal animal);
    }
}