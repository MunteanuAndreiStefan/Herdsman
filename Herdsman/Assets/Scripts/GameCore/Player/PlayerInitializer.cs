using UnityEngine;

namespace GameCore.Player
{
    /// <summary>
    /// Player initializer, it keeps the player prefab and initializes the player at a given position.
    /// </summary>
    public class PlayerInitializer : IPlayerInitializer
    {
        private readonly GameObject playerPrefab;

        /// <summary>
        /// Initializes the player prefab.
        /// </summary>
        /// <param name="playerPrefab">Player prefab</param>
        public PlayerInitializer(GameObject playerPrefab) =>
            this.playerPrefab = playerPrefab;

        /// <summary>
        /// Instantiates the player at position.
        /// </summary>
        /// <param name="position">Position to setup the player.</param>
        public void InitializePlayer(Vector3 position) =>
            Object.Instantiate(playerPrefab, position, Quaternion.identity);
    }
}