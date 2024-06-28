using System.Collections.Generic;
using UnityEngine;

namespace Utils.Collections
{
    /// <summary>
    /// ListExtensions, a class for extending List functionality
    /// </summary>
    public static class ListExtensions
    {
        /// <summary>
        /// Returns random element from a list
        /// </summary>
        /// <param name="list">List from which to pick</param>
        /// <typeparam name="T">Type of the list</typeparam>
        /// <returns>The random element of the list.</returns>
        public static T GetRandomElement<T>(List<T> list)
        {
            //This could be optimized, or made more specific like using curves or using time etc. https://docs.unity3d.com/Manual/class-Random.html

            var randomIndex = Random.Range(0, list.Count);
            return list[randomIndex];
        }
    }
}