using UnityEngine;

namespace GameCore.Spawn
{
    /// <summary>
    /// Used to optimze access to transform.postion each time a new instance is created.
    /// </summary>
    public class SpawnPoint : MonoBehaviour
    {
        public Vector3 Position { get; private set; }

        private void Awake() => Position = transform.position;
    }
}