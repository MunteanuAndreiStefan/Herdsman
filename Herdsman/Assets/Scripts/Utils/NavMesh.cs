using GameCore;
using UnityEngine;

namespace Utils
{
    /// <summary>
    /// NavMesh is a utility class for NavMesh operations.
    /// </summary>
    public static class NavMesh
    {
        /// <summary>
        /// Checks if a point is accessible to the NavMesh.
        /// </summary>
        /// <param name="point">Vector3 point to check if the agent has access to</param>
        /// <returns>Returns true if the agent has access to the point.</returns>
        public static bool IsPointAccessible(Vector3 point)
        {
            var areaMask = ~UnityEngine.AI.NavMesh.GetAreaFromName("Walkable");
            return UnityEngine.AI.NavMesh.SamplePosition(point, out _, DiContainer.Instance.GameConfig.FollowDistance, areaMask);
        }
    }
}