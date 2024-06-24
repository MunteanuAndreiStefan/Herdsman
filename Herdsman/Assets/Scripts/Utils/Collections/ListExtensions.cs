using System.Collections.Generic;
using UnityEngine;

namespace Utils.Collections
{
    public static class ListExtensions
    {
        public static T GetRandomElement<T>(List<T> list)
        {
            //This could be optimized, or made more specific like using curves or using time etc. https://docs.unity3d.com/Manual/class-Random.html

            var randomIndex = Random.Range(0, list.Count);
            return list[randomIndex];
        }
    }
}