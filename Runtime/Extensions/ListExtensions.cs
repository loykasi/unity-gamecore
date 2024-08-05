using System.Collections.Generic;
using UnityEngine;

namespace GameCore.Extensions
{
    public static class ListExtensions
    {
        public static int GetRandomIndex<T>(this List<T> list)
        {
            if (list.Count == 0)
                return -1;
            return Random.Range(0, list.Count);
        }

        public static int GetRandomIndex<T>(this List<T> list, int length)
        {
            if (list.Count == 0)
                return -1;
            length = Mathf.Min(length, list.Count);
            return Random.Range(0, length);
        }

        public static T GetRandom<T>(this List<T> list)
        {
            if (list.Count == 0)
                return default;
            return list[Random.Range(0, list.Count)];
        }

        public static T GetRandom<T>(this List<T> list, int length)
        {
            if (list.Count == 0)
                return default;
            length = Mathf.Min(length, list.Count);
            return list[Random.Range(0, length)];
        }
    }   
}