using UnityEngine;

namespace GameCore.SoundSystem
{
    [System.Serializable]
    public class Sound<T>
    {
        public T Key;
        public AudioClip Clip;
    }
}