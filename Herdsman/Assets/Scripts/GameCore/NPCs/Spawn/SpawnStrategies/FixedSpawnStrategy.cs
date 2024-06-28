using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Utils.Collections;

namespace GameCore.Spawn
{
    /// <summary>
    /// Strategy for fixed spawn points.
    /// </summary>
    public class FixedSpawnStrategy : ISpawnStrategy
    {
        [SerializeField] private List<SpawnPoint> _spawnPoints = new List<SpawnPoint>();

        /// <summary>
        /// Generates a list randomly of positions after which it generates at those positions.
        /// </summary>
        public FixedSpawnStrategy()
        {
            foreach (var spawnPoint in _spawnPoints.ToList().Where(spawnPoint => spawnPoint == null))
            {
                _spawnPoints.Remove(spawnPoint);
                Debug.LogError("One spawn point was null, it was removed");
            }

            if (_spawnPoints.Count == 0)
                Debug.LogError("No spawn points set for FixedSpawnStrategy");
        }

        public Vector3 GetSpawnPosition() =>
            ListExtensions.GetRandomElement(_spawnPoints).Position;

    }
}