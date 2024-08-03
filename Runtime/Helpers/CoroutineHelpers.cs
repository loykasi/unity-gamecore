using System.Collections.Generic;
using UnityEngine;

namespace GameCore.Helpers
{
    public static class CoroutineHelpers
    {
        private static readonly Dictionary<float, WaitForSeconds> _waitForSecondsTable = new Dictionary<float, WaitForSeconds>();

        public static WaitForSeconds GetWaitForSeconds(float duration)
        {
            if (_waitForSecondsTable.TryGetValue(duration, out WaitForSeconds wait))
            {
                return wait;
            }
            _waitForSecondsTable.Add(duration, new WaitForSeconds(duration));
            Debug.Log(duration);
            return _waitForSecondsTable[duration];
        }
    }   
}