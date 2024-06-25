using UnityEngine;

namespace GameCore.Spawn
{
    public interface ISpawnStrategy
    {
        public Vector3 GetSpawnPosition();
    }
}