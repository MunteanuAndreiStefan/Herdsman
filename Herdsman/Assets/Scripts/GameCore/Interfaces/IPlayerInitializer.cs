using UnityEngine;

namespace GameCore.Player
{
    /// <summary>
    /// Player initializer interface
    /// </summary>
    public interface IPlayerInitializer
    {
        public void InitializePlayer(Vector3 position);
    }
}