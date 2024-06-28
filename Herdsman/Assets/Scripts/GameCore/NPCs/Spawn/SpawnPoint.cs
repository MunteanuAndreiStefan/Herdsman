using UnityEngine;

namespace GameCore.Spawn
{
    /// <summary>
    /// Used to optimze access to transform.postion each time a new instance is created.
    /// We could keep all this data in a list in the FixedSpawnStrategy, but this way we can add extra functionality to the spawn points.
    /// </summary>
    public class SpawnPoint : MonoBehaviour
    {
        public Vector3 Position { get; private set; }

        private void Awake() => Position = transform.position;
    }
}