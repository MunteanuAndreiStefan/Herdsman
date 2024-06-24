using UnityEngine;

namespace GameCore.Player
{
    public class PlayerInitializer : IPlayerInitializer
    {
        private readonly GameObject playerPrefab;

        public PlayerInitializer(GameObject playerPrefab) =>
            this.playerPrefab = playerPrefab;

        public void InitializePlayer(Vector3 position) =>
            Object.Instantiate(playerPrefab, position, Quaternion.identity);
    }
}