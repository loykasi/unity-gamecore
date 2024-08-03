using UnityEngine;

namespace GameCore.Extensions
{
    public static class ArrayExtensions
    {
        public static int GetRandomIndex<T>(this T[] array)
        {
            if (array.Length == 0)
                return -1;
            return Random.Range(0, array.Length);
        }

        public static T GetRandom<T>(this T[] array)
        {
            if (array.Length == 0)
                return default;
            return array[Random.Range(0, array.Length)];
        }
    }   
}