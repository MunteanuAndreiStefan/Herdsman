using UnityEngine;

namespace GameCore.Player
{
    public interface IPlayerInitializer
    {
        void InitializePlayer(Vector3 position);
    }
}