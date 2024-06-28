using UnityEngine;
using Utils;

namespace GameCore.Spawn
{
    /// <summary>
    /// Strategy for random spawn points.
    /// </summary>
    public class RandomSpawnStrategy : ISpawnStrategy
    {
        public Vector3 GetSpawnPosition()
        {
            var point = new Vector3(
                Random.Range(-9, 9),
                Random.Range(-9, 9),
                0
            );

            while (!NavMesh.IsPointAccessible(point))
                point = new Vector3(
                    Random.Range(-9, 9),
                    Random.Range(-9, 9),
                    0
                );
            return point;
        }
    }
}