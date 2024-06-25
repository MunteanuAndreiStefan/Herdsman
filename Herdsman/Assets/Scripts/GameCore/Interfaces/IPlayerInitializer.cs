using UnityEngine;

namespace GameCore.Player
{
    public interface IPlayerInitializer
    {
        public void InitializePlayer(Vector3 position);
    }
}