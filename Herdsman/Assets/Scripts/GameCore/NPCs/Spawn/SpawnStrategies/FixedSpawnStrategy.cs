using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Utils.Collections;

namespace GameCore.Spawn
{
    public class FixedSpawnStrategy : ISpawnStrategy
    {
        [SerializeField] private List<SpawnPoint> _spawnPoints = new List<SpawnPoint>();

        public FixedSpawnStrategy()
        {
            foreach (var spawnPoint in _spawnPoints.ToList().Where(spawnPoint => spawnPoint == null)) // Done only once.
            {
                _spawnPoints.Remove(spawnPoint);
                Debug.LogError("one spawn point was null, it was removed");
            }

            if (_spawnPoints.Count == 0)
                Debug.LogError("No spawn points set for FixedSpawnStrategy");
        }

        public Vector3 GetSpawnPosition() =>
            ListExtensions.GetRandomElement(_spawnPoints).Position; // This could be used to contain also rotation data.

    }
}