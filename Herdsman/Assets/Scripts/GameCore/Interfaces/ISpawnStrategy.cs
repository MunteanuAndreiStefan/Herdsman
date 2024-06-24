using UnityEngine;

namespace GameCore.Spawn
{
    public interface ISpawnStrategy
    {
        Vector3 GetSpawnPosition();
    }
}