using System.Collections;
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

        public static Coroutine DoAnimation(this MonoBehaviour monoBehaviour, float duration, System.Action<float> onSet, System.Action onComplete)
        {
            return monoBehaviour.StartCoroutine(RunAnimation(duration, onSet, onComplete));
        }

        public static IEnumerator RunAnimation(float duration, System.Action<float> onSet, System.Action onComplete)
        {
            float time = 0;
            float dt = 1f / duration;
            while (time < 1)
            {
                onSet(time);
                time += dt * Time.deltaTime;
                yield return null;
            }
            onSet(1);
            onComplete?.Invoke();
        }
    }
}