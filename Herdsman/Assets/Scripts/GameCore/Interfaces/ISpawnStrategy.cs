using UnityEngine;

namespace GameCore.Spawn
{
    /// <summary>
    /// Spawn strategy interface
    /// </summary>
    public interface ISpawnStrategy
    {
        public Vector3 GetSpawnPosition();
    }
}