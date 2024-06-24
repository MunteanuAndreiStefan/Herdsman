using UnityEngine;

namespace Utils
{
    public static class NavMesh
    {
        public static bool IsPointAccessible(Vector3 point)
        {
            var areaMask = ~UnityEngine.AI.NavMesh.GetAreaFromName("Walkable");
            return UnityEngine.AI.NavMesh.SamplePosition(point, out _, Constants.DEFAULT_FOLLOW_DISTANCE, areaMask);
        }
    }
}